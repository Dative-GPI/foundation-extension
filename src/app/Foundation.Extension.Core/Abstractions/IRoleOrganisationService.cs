using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRoleOrganisationService
    {
        Task<RoleOrganisationDetailsViewModel> Get(Guid roleOrganisationId);
        Task<RoleOrganisationDetailsViewModel> Update(Guid roleOrganisationId, UpdateRoleOrganisationViewModel payload);
    }
}