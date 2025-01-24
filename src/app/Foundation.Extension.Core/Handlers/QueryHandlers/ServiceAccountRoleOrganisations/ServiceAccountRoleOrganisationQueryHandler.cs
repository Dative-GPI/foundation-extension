using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class ServiceAccountRoleOrganisationQueryHandler : IMiddleware<ServiceAccountRoleOrganisationQuery, ServiceAccountRoleOrganisationDetails>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public ServiceAccountRoleOrganisationQueryHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<ServiceAccountRoleOrganisationDetails> HandleAsync(ServiceAccountRoleOrganisationQuery request, Func<Task<ServiceAccountRoleOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var baseRole = await _rolePermissionOrganisationRepository.Get(request.ServiceAccountRoleOrganisationId);

            return new ServiceAccountRoleOrganisationDetails()
            {
                Id = request.ServiceAccountRoleOrganisationId,
                Permissions = baseRole.Permissions
            };
        }
    }
}