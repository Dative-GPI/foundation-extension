using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IUserOrganisationService
    {
        Task<UserOrganisationDetailsViewModel> GetCurrent();
        Task<UserOrganisationDetailsViewModel> Get(Guid userOrganisationId);
    }
}