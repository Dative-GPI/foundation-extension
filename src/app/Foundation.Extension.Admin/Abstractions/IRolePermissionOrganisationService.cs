using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IRolePermissionOrganisationService
    {
        Task<RolePermissionOrganisationDetailsViewModel> Get(Guid id);
        Task<RolePermissionOrganisationDetailsViewModel> Update(Guid id, UpdateRolePermissionOrganisationViewModel payload);
    }
}