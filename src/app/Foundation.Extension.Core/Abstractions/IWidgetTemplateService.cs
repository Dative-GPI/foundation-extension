using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Clients.Core.FoundationModels;

namespace Foundation.Extension.Core.Abstractions
{
	public interface IWidgetTemplateService
    {
        Task<IEnumerable<WidgetTemplateInfosFoundationModel>> GetMany(WidgetTemplatesFilterFoundationModel filter);
    }
}