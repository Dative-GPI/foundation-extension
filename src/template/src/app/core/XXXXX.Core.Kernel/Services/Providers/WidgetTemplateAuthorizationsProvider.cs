using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Models;

namespace XXXXX.Core.Kernel.Services.Providers
{
    public class WidgetTemplateAuthorizationsProvider : IWidgetTemplateAuthorizationsProvider
    {
        private readonly IPermissionProvider _permissionProvider;

        public WidgetTemplateAuthorizationsProvider(IPermissionProvider permissionProvider)
        {
            _permissionProvider = permissionProvider;
        }

        // Define per-template permissions by mapping widget template codes to their required authorizations.
        // Templates not listed here, or listed without authorizations, are accessible to all users.
        // Example:
        // private static IEnumerable<WidgetTemplateDefinition> Definitions => new List<WidgetTemplateDefinition>()
        // {
        //     new WidgetTemplateDefinition()
        //     {
        //         Code = "ui.widgets.extension",
        //         Authorizations = new List<string>() { Authorizations.XXXXX_PERMISSION }
        //     }
        // };
        private static IEnumerable<WidgetTemplateDefinition> Definitions => new List<WidgetTemplateDefinition>();

        public async Task<IEnumerable<WidgetTemplateInfos>> FilterAsync(IEnumerable<WidgetTemplateInfos> widgetTemplates)
        {
            if (!Definitions.Any())
            {
                return widgetTemplates;
            }

            var permissions = new HashSet<string>(await _permissionProvider.GetPermissions());

            return widgetTemplates.Where(wt => HasPermissions(wt, permissions));
        }

        private static bool HasPermissions(WidgetTemplateInfos widgetTemplate, HashSet<string> grantedPermissions)
        {
            var definition = Definitions.FirstOrDefault(d => d.Code == widgetTemplate.Code);
            if (definition?.Authorizations == null || !definition.Authorizations.Any())
            {
                return true;
            }
            return definition.Authorizations.All(a => grantedPermissions.Contains(a));
        }
    }
}
