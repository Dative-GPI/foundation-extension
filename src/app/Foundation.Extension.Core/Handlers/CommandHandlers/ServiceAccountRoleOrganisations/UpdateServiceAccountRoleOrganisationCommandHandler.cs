using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class UpdateServiceAccountRoleOrganisationCommandHandler : IMiddleware<UpdateServiceAccountRoleOrganisationCommand, IEntity<Guid>>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public UpdateServiceAccountRoleOrganisationCommandHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateServiceAccountRoleOrganisationCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var update = new UpdateRolePermissionOrganisation()
            {
                Id = command.ServiceAccountRoleOrganisationId,
                PermissionIds = command.PermissionIds
            };
            
            var baseRole = await _rolePermissionOrganisationRepository.Update(update);

            return baseRole;
        }
    }
}