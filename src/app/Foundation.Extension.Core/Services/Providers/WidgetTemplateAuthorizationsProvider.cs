using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Services.Providers
{
    public class WidgetTemplateAuthorizationsProvider : IWidgetTemplateAuthorizationsProvider
    {
        public Task<IEnumerable<WidgetTemplateInfos>> FilterAsync(IEnumerable<WidgetTemplateInfos> widgetTemplates)
        {
            return Task.FromResult(widgetTemplates);
        }
    }
}
