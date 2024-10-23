using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Abstractions;

namespace XXXXX.Core.Kernel.Services
{
	public class ActionsProvider : IActionsProvider
	{
		public Task<IEnumerable<ActionInfos>> GetActions(string path)
		{
			throw new System.NotImplementedException();
		}
	}
}