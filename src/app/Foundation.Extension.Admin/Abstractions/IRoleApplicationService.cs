using System;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IRoleApplicationService
    {
        Task<RoleApplicationDetailsViewModel> Get(Guid roleApplicationId);
        Task<RoleApplicationDetailsViewModel> Update(Guid roleApplicationId, UpdateRoleApplicationViewModel payload);
    }
}