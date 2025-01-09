using System;
using System.Threading.Tasks;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IUserPermissionOrganisationService
    {
        Task<UserPermissionOrganisationDetailsViewModel> GetUserPermissionOrganisation(Guid userId, UserType userType);
    }
}