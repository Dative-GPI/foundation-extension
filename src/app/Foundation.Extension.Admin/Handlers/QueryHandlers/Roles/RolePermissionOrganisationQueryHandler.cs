using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class RolePermissionOrganisationQueryHandler : IMiddleware<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails>
    {
        private IRolePermissionOrganisationRepository _roleOrganisationRepository;

        public RolePermissionOrganisationQueryHandler(
            IRolePermissionOrganisationRepository roleOrganisationRepository)
        {
            _roleOrganisationRepository = roleOrganisationRepository;
        }

        public async Task<RolePermissionOrganisationDetails> HandleAsync(RolePermissionOrganisationQuery request, Func<Task<RolePermissionOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var result = await _roleOrganisationRepository.Get(request.RoleOrganisationId);
            return result;
        }
    }
}