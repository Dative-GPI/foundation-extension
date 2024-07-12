using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class TablesQueryHandler : IMiddleware<TablesQuery, IEnumerable<ApplicationTableInfos>>
    {
        private IColumnRepository _columnRepository;
        private ITableRepository _tableRepository;

        public TablesQueryHandler(ITableRepository tableRepository,
            IColumnRepository columnRepository
        )
        {
            _columnRepository = columnRepository;
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<ApplicationTableInfos>> HandleAsync(TablesQuery request, Func<Task<IEnumerable<ApplicationTableInfos>>> next, CancellationToken cancellationToken)
        {
            var tables = await _tableRepository.GetMany(new TablesFilter());

            var columns = await _columnRepository.GetMany(new ColumnsFilter()
            {
                ApplicationId = request.ApplicationId
            });

            var configuredTables = columns.Select(c => c.TableId).Distinct();

            var appTables = tables.GroupJoin(configuredTables, t => t.Id, t => t, (t, cs) => new ApplicationTableInfos()
            {
                Code = t.Code,
                EntityType = t.EntityType,
                Id = t.Id,
                Label = t.Label
            });

            return appTables;
        }
    }
}