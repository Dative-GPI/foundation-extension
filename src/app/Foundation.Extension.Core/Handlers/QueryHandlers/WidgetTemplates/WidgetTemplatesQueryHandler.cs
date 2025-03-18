using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Handlers
{
    public class WidgetTemplatesQueryHandler : IMiddleware<WidgetTemplatesQuery, IEnumerable<WidgetTemplateInfos>>
    {
        private readonly IWidgetTemplateRepository _widgetTemplateRepository;

        public WidgetTemplatesQueryHandler
        (
            IWidgetTemplateRepository widgetTemplateRepository
        )
        {
            _widgetTemplateRepository = widgetTemplateRepository;
        }

        public async Task<IEnumerable<WidgetTemplateInfos>> HandleAsync(WidgetTemplatesQuery request, Func<Task<IEnumerable<WidgetTemplateInfos>>> next, CancellationToken cancellationToken)
        {
            var filter = new WidgetTemplatesFilter()
            {
                Search = request.Search
            };

            var WidgetTemplates = await _widgetTemplateRepository.GetMany(filter);

            return WidgetTemplates;
        }
    }
}