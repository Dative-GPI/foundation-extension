using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IActionsProvider
    {
        Task<IEnumerable<ActionInfos>> GetActions(string path);
    }
}