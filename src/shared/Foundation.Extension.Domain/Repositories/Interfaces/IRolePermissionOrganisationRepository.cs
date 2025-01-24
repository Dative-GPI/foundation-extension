using System;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IRolePermissionOrganisationRepository
    {
        Task<BasePermissionDetails> Get(Guid roleId);
        Task<IEntity<Guid>> Update(UpdateRolePermissionOrganisation payload);
    }
}