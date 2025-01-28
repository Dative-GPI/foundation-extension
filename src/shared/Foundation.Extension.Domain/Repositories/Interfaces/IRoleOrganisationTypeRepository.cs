using System;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IRoleOrganisationTypeRepository
    {
        Task<RoleOrganisationTypeDetails> Get(Guid roleOrganisationTypeId);
        Task<IEntity<Guid>> Update(UpdateRoleOrganisationType payload);
    }
}