using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IServiceAccountRoleOrganisationService
    {
        Task<ServiceAccountRoleOrganisationDetailsViewModel> Get(Guid serviceAccountRoleOrganisationId);
        Task<ServiceAccountRoleOrganisationDetailsViewModel> Update(Guid serviceAccountRoleOrganisationId, UpdateServiceAccountRoleOrganisationViewModel payload);
    }
}