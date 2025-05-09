using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
	public interface IWidgetTemplateRepository
	{
		Task<IEnumerable<WidgetTemplateInfos>> GetMany(WidgetTemplatesFilter filter);
	}
}