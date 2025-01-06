using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRolePermissionOrganisationService
    {
        Task<RolePermissionOrganisationDetailsViewModel> GetRolePermissionOrganisation(Guid roleId);
        Task<RolePermissionOrganisationDetailsViewModel> UpdateRolePermissionOrganisation(Guid id, UpdateRolePermissionOrganisationViewModel payload);
    }
}