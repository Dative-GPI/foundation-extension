using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IPermissionApplicationService
    {
        Task<IEnumerable<string>> GetCurrent();
        Task<IEnumerable<PermissionApplicationInfosViewModel>> GetMany(PermissionApplicationFilterViewModel filter);
    }
}
