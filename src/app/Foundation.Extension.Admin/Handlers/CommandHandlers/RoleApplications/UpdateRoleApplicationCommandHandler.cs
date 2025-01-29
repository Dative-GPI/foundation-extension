using System;
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
        private readonly IRoleApplicationRepository _roleApplicationRepository;

        public UpdateRoleApplicationCommandHandler(IRoleApplicationRepository roleApplicationRepository)
        {
            _roleApplicationRepository = roleApplicationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleApplicationCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var update = new UpdateRoleApplication()
            {
                Id = command.RoleApplicationId,
                PermissionIds = command.PermissionIds
            };
            
            var roleApplication = await _roleApplicationRepository.Update(update);

            return roleApplication;
        }
    }
}