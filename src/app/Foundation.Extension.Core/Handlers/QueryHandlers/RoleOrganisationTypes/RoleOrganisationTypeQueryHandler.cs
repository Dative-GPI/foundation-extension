using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class RoleOrganisationTypeQueryHandler : IMiddleware<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails>
    {
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;

        public RoleOrganisationTypeQueryHandler(IRolePermissionOrganisationRepository rolePermissionOrganisationRepository)
        {
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
        }

        public async Task<RoleOrganisationTypeDetails> HandleAsync(RoleOrganisationTypeQuery request, Func<Task<RoleOrganisationTypeDetails>> next, CancellationToken cancellationToken)
        {
            var baseRole = await _rolePermissionOrganisationRepository.Get(request.RoleOrganisationTypeId);

            return new RoleOrganisationTypeDetails()
            {
                Id = request.RoleOrganisationTypeId,
                Permissions = baseRole.Permissions
            };
        }
    }
}