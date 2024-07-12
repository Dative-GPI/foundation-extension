using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRolePermissionOrganisationService
    {
        Task<RolePermissionOrganisationDetailsViewModel> Get(Guid roleId);
        Task<RolePermissionOrganisationDetailsViewModel> Update(Guid id, UpdateRolePermissionOrganisationViewModel payload);
    }
}