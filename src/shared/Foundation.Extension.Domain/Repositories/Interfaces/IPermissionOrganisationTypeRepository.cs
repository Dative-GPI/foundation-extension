using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IPermissionOrganisationTypeRepository
    {
        Task<IEnumerable<PermissionOrganisationTypeInfos>> GetMany(PermissionOrganisationTypesFilter filter);
        Task CreateRange(IEnumerable<CreatePermissionOrganisationType> payload);
        Task RemoveRange(Guid[] permissionsIds);
    }
}