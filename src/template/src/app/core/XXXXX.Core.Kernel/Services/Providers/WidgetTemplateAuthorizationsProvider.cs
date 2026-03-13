using System.Collections.Generic;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Models;

namespace XXXXX.Core.Kernel.Services.Providers
{
    public class WidgetTemplateAuthorizationsProvider : IWidgetTemplateAuthorizationsProvider
    {
        // Define per-template permissions by mapping widget template codes to their required authorizations.
        // Templates not listed here, or listed without authorizations, are accessible to all users.
        // Example:
        // public IEnumerable<WidgetTemplateDefinition> Definitions => new List<WidgetTemplateDefinition>()
        // {
        //     new WidgetTemplateDefinition()
        //     {
        //         Code = "ui.widgets.extension",
        //         Authorizations = new List<string>() { Authorizations.XXXXX_PERMISSION }
        //     }
        // };
        public IEnumerable<WidgetTemplateDefinition> Definitions => new List<WidgetTemplateDefinition>();
    }
}
