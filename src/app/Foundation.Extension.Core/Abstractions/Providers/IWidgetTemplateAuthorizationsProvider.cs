using System.Collections.Generic;

using Foundation.Extension.Core.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IWidgetTemplateAuthorizationsProvider
    {
        IEnumerable<WidgetTemplateDefinition> Definitions { get; }
    }
}
