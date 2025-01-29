using System;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IRoleApplicationRepository
    {
        Task<RoleApplicationDetails> Get(Guid roleApplicationId);
        Task<IEntity<Guid>> Update(UpdateRoleApplication payload);
    }
}