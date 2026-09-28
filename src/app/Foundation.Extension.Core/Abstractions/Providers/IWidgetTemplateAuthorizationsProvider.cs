using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IWidgetTemplateAuthorizationsProvider
    {
        Task<IEnumerable<WidgetTemplateInfos>> FilterAsync(IEnumerable<WidgetTemplateInfos> widgetTemplates);
    }
}
