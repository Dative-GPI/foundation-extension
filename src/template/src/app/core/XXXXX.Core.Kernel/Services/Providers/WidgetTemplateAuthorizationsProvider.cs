using System.Collections.Generic;

using Foundation.Extension.Core.Abstractions;

namespace XXXXX.Core.Kernel.Services.Providers
{
    public class WidgetTemplateAuthorizationsProvider : IWidgetTemplateAuthorizationsProvider
    {
        // Define the permissions required to access widget templates from this extension.
        // Reference permission codes from Authorizations.cs, e.g.:
        // public IEnumerable<string> Authorizations => new List<string>() { Authorizations.XXXXX_PERMISSION };
        public IEnumerable<string> Authorizations => new List<string>();
    }
}
