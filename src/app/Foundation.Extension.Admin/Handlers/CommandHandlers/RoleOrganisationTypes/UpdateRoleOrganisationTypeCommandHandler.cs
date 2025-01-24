using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UpdateRoleOrganisationTypeCommandHandler : IMiddleware<UpdateRoleOrganisationTypeCommand, IEntity<Guid>>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public UpdateRoleOrganisationTypeCommandHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleOrganisationTypeCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var update = new UpdateRolePermissionOrganisation()
            {
                Id = command.RoleOrganisationTypeId,
                PermissionIds = command.PermissionIds
            };

            var baseRole = await _rolePermissionOrganisationRepository.Update(update);

            return baseRole;
        }
    }
}