using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IServiceAccountOrganisationService
    {
        Task<ServiceAccountOrganisationDetailsViewModel> Get(Guid serviceAccountOrganisationId);
    }
}