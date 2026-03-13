using System.Collections.Generic;

using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Services.Providers
{
    public class WidgetTemplateAuthorizationsProvider : IWidgetTemplateAuthorizationsProvider
    {
        public IEnumerable<string> Authorizations => new List<string>();
    }
}
