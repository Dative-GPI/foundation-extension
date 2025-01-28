using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IPermissionApplicationCategoryService
    {
        Task<IEnumerable<PermissionApplicationCategoryInfosViewModel>> GetMany();
    }
}