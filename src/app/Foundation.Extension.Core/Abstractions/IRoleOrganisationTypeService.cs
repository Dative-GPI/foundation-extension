using System;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRoleOrganisationTypeService
    {
        Task<RoleOrganisationTypeDetailsViewModel> Get(Guid roleOrganisationTypeId);
    }
}