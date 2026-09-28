using System;
using System.Collections.Generic;
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
        private readonly IWidgetTemplateAuthorizationsProvider _widgetTemplateAuthorizationsProvider;

        public WidgetTemplatesQueryHandler
        (
            IWidgetTemplateRepository widgetTemplateRepository,
            IWidgetTemplateAuthorizationsProvider widgetTemplateAuthorizationsProvider
        )
        {
            _widgetTemplateRepository = widgetTemplateRepository;
            _widgetTemplateAuthorizationsProvider = widgetTemplateAuthorizationsProvider;
        }

        public async Task<IEnumerable<WidgetTemplateInfos>> HandleAsync(WidgetTemplatesQuery request, Func<Task<IEnumerable<WidgetTemplateInfos>>> next, CancellationToken cancellationToken)
        {
            var filter = new WidgetTemplatesFilter()
            {
                Search = request.Search
            };

            var widgetTemplates = await _widgetTemplateRepository.GetMany(filter);

            return await _widgetTemplateAuthorizationsProvider.FilterAsync(widgetTemplates);
        }
    }
}
