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
        private readonly IRoleOrganisationTypeRepository _roleOrganisationTypeRepository;

        public UpdateRoleOrganisationTypeCommandHandler(IRoleOrganisationTypeRepository roleOrganisationTypeRepository)
        {
            _roleOrganisationTypeRepository = roleOrganisationTypeRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRoleOrganisationTypeCommand command, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var update = new UpdateRoleOrganisationType()
            {
                Id = command.RoleOrganisationTypeId,
                PermissionIds = command.PermissionIds
            };

            var roleOrganisationType = await _roleOrganisationTypeRepository.Update(update);

            return roleOrganisationType;
        }
    }
}