using System;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IRoleOrganisationTypeService
    {
        Task<RoleOrganisationTypeDetailsViewModel> Get(Guid roleOrganisationTypeId);
        Task<RoleOrganisationTypeDetailsViewModel> Update(Guid roleOrganisationTypeId, UpdateRoleOrganisationTypeViewModel payload);
    }
}