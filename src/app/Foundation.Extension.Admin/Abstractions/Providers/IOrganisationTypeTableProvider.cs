using System;
using System.Threading.Tasks;
using Foundation.Extension.Admin.Models;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IOrganisationTypeTableProvider
    {
        Task<OrganisationTypeTableDetails> Get(Guid organisationTypeId, Guid tableId);
    }
}