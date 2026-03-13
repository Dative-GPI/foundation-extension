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
using Foundation.Extension.Core.Models;

namespace Foundation.Extension.Core.Handlers
{
    public class WidgetTemplatesQueryHandler : IMiddleware<WidgetTemplatesQuery, IEnumerable<WidgetTemplateInfos>>
    {
        private readonly IWidgetTemplateRepository _widgetTemplateRepository;
        private readonly IWidgetTemplateAuthorizationsProvider _widgetTemplateAuthorizationsProvider;
        private readonly IPermissionProvider _permissionProvider;

        public WidgetTemplatesQueryHandler
        (
            IWidgetTemplateRepository widgetTemplateRepository,
            IWidgetTemplateAuthorizationsProvider widgetTemplateAuthorizationsProvider,
            IPermissionProvider permissionProvider
        )
        {
            _widgetTemplateRepository = widgetTemplateRepository;
            _widgetTemplateAuthorizationsProvider = widgetTemplateAuthorizationsProvider;
            _permissionProvider = permissionProvider;
        }

        public async Task<IEnumerable<WidgetTemplateInfos>> HandleAsync(WidgetTemplatesQuery request, Func<Task<IEnumerable<WidgetTemplateInfos>>> next, CancellationToken cancellationToken)
        {
            var filter = new WidgetTemplatesFilter()
            {
                Search = request.Search
            };

            var widgetTemplates = await _widgetTemplateRepository.GetMany(filter);

            var definitionsByCode = _widgetTemplateAuthorizationsProvider.Definitions
                .ToDictionary(d => d.Code);

            if (definitionsByCode.Count == 0)
            {
                return widgetTemplates;
            }

            var grantedPermissions = await _permissionProvider.GetPermissions();

            return widgetTemplates.Where(wt => HasPermissions(wt, definitionsByCode, grantedPermissions));
        }

        private static bool HasPermissions(WidgetTemplateInfos widgetTemplate, Dictionary<string, WidgetTemplateDefinition> definitionsByCode, IEnumerable<string> grantedPermissions)
        {
            if (!definitionsByCode.TryGetValue(widgetTemplate.Code, out var definition))
            {
                return true;
            }
            if (!definition.Authorizations.Any())
            {
                return true;
            }
            return !definition.Authorizations.Except(grantedPermissions).Any();
        }
    }
}
