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
        private readonly IRoleOrganisationRepository _roleOrganisationRepository;

        public UpdateRoleOrganisationCommandHandler(IRoleOrganisationRepository roleOrganisationRepository)
        {
            _roleOrganisationRepository = roleOrganisationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleOrganisationCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var update = new UpdateRoleOrganisation()
            {
                Id = command.RoleOrganisationId,
                PermissionIds = command.PermissionIds
            };

            var roleOrganisation = await _roleOrganisationRepository.Update(update);

            return roleOrganisation;
        }
    }
}