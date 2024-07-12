using System;
using System.Threading.Tasks;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IOrganisationTypeTableProvider
    {
        Task<OrganisationTypeTableDetails> Get(Guid organisationTypeId, Guid tableId);
    }
}