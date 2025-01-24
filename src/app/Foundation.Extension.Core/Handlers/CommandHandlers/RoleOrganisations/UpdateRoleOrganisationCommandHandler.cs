using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class UpdateRoleOrganisationCommandHandler : IMiddleware<UpdateRoleOrganisationCommand, IEntity<Guid>>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public UpdateRoleOrganisationCommandHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleOrganisationCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var baseRole = await _rolePermissionOrganisationRepository.Update(new UpdateRolePermissionOrganisation()
            {
                Id = command.RoleOrganisationId,
                PermissionIds = command.PermissionIds
            });

            return baseRole;
        }
    }
}