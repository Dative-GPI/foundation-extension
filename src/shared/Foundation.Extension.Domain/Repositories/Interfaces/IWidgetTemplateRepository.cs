using System;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
	public interface IWidgetTemplateRepository
	{
		Task<WidgetTemplateDetails> Get(Guid id);
	}
}