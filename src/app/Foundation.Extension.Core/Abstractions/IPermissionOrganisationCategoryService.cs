using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IPermissionOrganisationCategoryService
    {
        Task<IEnumerable<PermissionOrganisationCategoryInfosViewModel>> GetMany();
    }
}