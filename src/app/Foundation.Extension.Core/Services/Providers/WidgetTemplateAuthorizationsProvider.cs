using System.Collections.Generic;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Models;

namespace Foundation.Extension.Core.Services.Providers
{
    public class WidgetTemplateAuthorizationsProvider : IWidgetTemplateAuthorizationsProvider
    {
        public IEnumerable<WidgetTemplateDefinition> Definitions => new List<WidgetTemplateDefinition>();
    }
}
