using System;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IRoleOrganisationRepository
    {
        Task<RoleOrganisationDetails> Get(Guid roleOrganisationId);
        Task<IEntity<Guid>> Update(UpdateRoleOrganisation payload);
    }
}