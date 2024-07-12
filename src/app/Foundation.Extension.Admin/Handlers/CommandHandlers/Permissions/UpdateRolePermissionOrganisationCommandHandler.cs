using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UpdateRolePermissionOrganisationCommandHandler : IMiddleware<UpdateRolePermissionOrganisationCommand, IEntity<Guid>>
    {
        private IRolePermissionOrganisationRepository _roleOrganisationRepository;

        public UpdateRolePermissionOrganisationCommandHandler(IRolePermissionOrganisationRepository roleOrganisationRepository)
        {
            _roleOrganisationRepository = roleOrganisationRepository;
        }

        public async Task<IEntity<Guid>> HandleAsync(UpdateRolePermissionOrganisationCommand request, Func<Task<IEntity<Guid>>> next, CancellationToken cancellationToken)
        {
            var entity = await _roleOrganisationRepository.Update(new UpdateRolePermissionOrganisation()
            {
                Id = request.RoleOrganisationId,
                PermissionIds = request.PermissionIds
            });

            return entity;
        }
    }
}