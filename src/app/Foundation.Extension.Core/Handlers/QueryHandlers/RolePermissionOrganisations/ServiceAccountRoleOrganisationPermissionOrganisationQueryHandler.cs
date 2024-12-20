using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class ServiceAccountRoleOrganisationPermissionOrganisationQueryHandler : IMiddleware<ServiceAccountRoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public ServiceAccountRoleOrganisationPermissionOrganisationQueryHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<RolePermissionOrganisationDetails> HandleAsync(ServiceAccountRoleOrganisationPermissionOrganisationQuery request, Func<Task<RolePermissionOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var rolePermissionOrganisation = await _rolePermissionOrganisationRepository.Get(request.RoleId);

            return rolePermissionOrganisation;
        }
    }
}