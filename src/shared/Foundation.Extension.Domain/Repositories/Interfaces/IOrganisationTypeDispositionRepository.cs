using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
  public interface IOrganisationTypeDispositionRepository
  {
    Task<IEnumerable<OrganisationTypeColumnInfos>> GetMany(ColumnOrganisationTypesFilter filter);
    Task Create(CreateOrganisationTypeDisposition payload);
    Task CreateMany(IEnumerable<CreateOrganisationTypeDisposition> payload);
    Task Remove(Guid organisationTypeDispositionId);
    Task RemoveMany(IEnumerable<Guid> organisationTypeDispositionsIds);
  }
}