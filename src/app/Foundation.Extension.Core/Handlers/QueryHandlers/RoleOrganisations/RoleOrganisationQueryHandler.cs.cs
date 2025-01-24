using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class RoleOrganisationQueryHandler : IMiddleware<RoleOrganisationQuery, RoleOrganisationDetails>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public RoleOrganisationQueryHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<RoleOrganisationDetails> HandleAsync(RoleOrganisationQuery request, Func<Task<RoleOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var baseRole = await _rolePermissionOrganisationRepository.Get(request.RoleOrganisationId);

            return new RoleOrganisationDetails()
            {
                Id = request.RoleOrganisationId,
                Permissions = baseRole.Permissions
            };
        }
    }
}