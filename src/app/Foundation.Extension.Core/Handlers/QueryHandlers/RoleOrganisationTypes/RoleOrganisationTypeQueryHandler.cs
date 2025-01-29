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
        private readonly IRoleOrganisationTypeRepository _roleOrganisationTypeRepository;

        public RoleOrganisationTypeQueryHandler(IRoleOrganisationTypeRepository roleOrganisationTypeRepository)
        {
            _roleOrganisationTypeRepository = roleOrganisationTypeRepository;
        }

        public async Task<RoleOrganisationTypeDetails> HandleAsync(RoleOrganisationTypeQuery request, Func<Task<RoleOrganisationTypeDetails>> next, CancellationToken cancellationToken)
        {
            var roleOrganisationType = await _roleOrganisationTypeRepository.Get(request.RoleOrganisationTypeId);

            return roleOrganisationType;
        }
    }
}