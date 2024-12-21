using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRolePermissionOrganisationService
    {
        Task<RolePermissionOrganisationDetailsViewModel> GetServiceAccountRoleOrganisation(Guid roleId);
        Task<RolePermissionOrganisationDetailsViewModel> GetRoleOrganisationType(Guid roleId);
        Task<RolePermissionOrganisationDetailsViewModel> GetRoleOrganisation(Guid roleId);
        Task<RolePermissionOrganisationDetailsViewModel> UpdateServiceAccountRoleOrganisation(Guid id, UpdateRolePermissionOrganisationViewModel payload);
        Task<RolePermissionOrganisationDetailsViewModel> UpdateRoleOrganisation(Guid id, UpdateRolePermissionOrganisationViewModel payload);
    }
}