using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IOrganisationTypeTableService
    {
        Task<OrganisationTypeTableDetailsViewModel> Get(Guid organisationTypeId, Guid tableId);
        Task<OrganisationTypeTableDetailsViewModel> Update(Guid organisationTypeId, Guid tableId, UpdateOrganisationTypeTableViewModel payload);
    }
}