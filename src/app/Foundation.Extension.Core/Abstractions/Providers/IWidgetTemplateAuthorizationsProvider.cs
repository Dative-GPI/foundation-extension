using System.Collections.Generic;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IWidgetTemplateAuthorizationsProvider
    {
        IEnumerable<string> Authorizations { get; }
    }
}
