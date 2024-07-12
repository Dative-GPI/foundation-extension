using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UpdateRoleApplicationCommandHandler : IMiddleware<UpdateRoleApplicationCommand, IEntity<Guid>>
    {
        private IRoleApplicationRepository _roleApplicationRepository;

        public UpdateRoleApplicationCommandHandler(IRoleApplicationRepository roleApplicationRepository)
        {
            _roleApplicationRepository = roleApplicationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleApplicationCommand request, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var entity = await _roleApplicationRepository.Update(new UpdateRoleApplication()
            {
                Id = request.RoleApplicationId,
                PermissionIds = request.PermissionIds
            });

            return entity;
        }
    }
}