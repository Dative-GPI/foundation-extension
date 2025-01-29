using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IPermissionProvider
    {
        Task<IEnumerable<string>> GetPermissions();
        Task<bool> HasPermissions(params string[] permissions);
    }
}