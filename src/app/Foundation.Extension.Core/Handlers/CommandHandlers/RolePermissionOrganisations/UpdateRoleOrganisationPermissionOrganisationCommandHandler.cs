using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class UpdateRoleOrganisationPermissionOrganisationCommandHandler : IMiddleware<UpdateRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public UpdateRoleOrganisationPermissionOrganisationCommandHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleOrganisationPermissionOrganisationCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var entity = await _rolePermissionOrganisationRepository.Update(new UpdateRolePermissionOrganisation()
            {
                Id = command.RoleId,
                PermissionIds = command.PermissionIds
            });

            return entity;
        }
    }
}