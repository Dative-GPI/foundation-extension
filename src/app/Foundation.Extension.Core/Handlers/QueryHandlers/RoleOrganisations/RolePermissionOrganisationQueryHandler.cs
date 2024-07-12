using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class RolePermissionOrganisationQueryHandler : IMiddleware<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails>
    {
        private readonly IRolePermissionOrganisationRepository _roleOrganisationRepository;

        public RolePermissionOrganisationQueryHandler(
            IRolePermissionOrganisationRepository roleOrganisationRepository
        )
        {
            _roleOrganisationRepository = roleOrganisationRepository;
        }

        public async Task<RolePermissionOrganisationDetails> HandleAsync(RolePermissionOrganisationQuery request, Func<Task<RolePermissionOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var role = await _roleOrganisationRepository.Get(request.RoleOrganisationId);

            return role;
        }
    }
}