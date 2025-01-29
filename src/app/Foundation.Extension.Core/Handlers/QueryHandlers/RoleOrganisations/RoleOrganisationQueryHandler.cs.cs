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
        private readonly IRoleOrganisationRepository _roleOrganisationRepository;

        public RoleOrganisationQueryHandler(IRoleOrganisationRepository roleOrganisationRepository)
        {
            _roleOrganisationRepository = roleOrganisationRepository;
        }

        public async Task<RoleOrganisationDetails> HandleAsync(RoleOrganisationQuery request, Func<Task<RoleOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var roleOrganisation = await _roleOrganisationRepository.Get(request.RoleOrganisationId);

            return roleOrganisation;
        }
    }
}