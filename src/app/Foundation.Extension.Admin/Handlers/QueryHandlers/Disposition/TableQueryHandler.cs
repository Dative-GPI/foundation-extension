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
    public class TableQueryHandler : IMiddleware<TableQuery, ApplicationTableDetails>
    {
        private readonly IColumnRepository _columnRepository;
        private readonly ITableRepository _tableRepository;

        public TableQueryHandler
        (
            ITableRepository tableRepository,
            IColumnRepository columnRepository
        )
        {
            _columnRepository = columnRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ApplicationTableDetails> HandleAsync(TableQuery request, Func<Task<ApplicationTableDetails>> next, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.Get(request.TableId);

            var columns = await _columnRepository.GetMany(new ColumnsFilter()
            {
                ApplicationId = request.ApplicationId,
                TableId = request.TableId
            });

            var appTable = new ApplicationTableDetails()
            {
                Code = table.Code,
                Columns = columns.ToList(),
                EntityType = table.EntityType,
                Id = table.Id,
                Label = table.Label
            };

            return appTable;
        }
    }
}