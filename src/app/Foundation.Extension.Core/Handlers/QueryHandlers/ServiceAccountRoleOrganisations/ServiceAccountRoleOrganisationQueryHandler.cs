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
        private readonly IServiceAccountRoleOrganisationRepository _serviceAccountRoleOrganisationRepository;

        public ServiceAccountRoleOrganisationQueryHandler(IServiceAccountRoleOrganisationRepository serviceAccountRoleOrganisationRepository)
        {
            _serviceAccountRoleOrganisationRepository = serviceAccountRoleOrganisationRepository;
        }

        public async Task<ServiceAccountRoleOrganisationDetails> HandleAsync(ServiceAccountRoleOrganisationQuery request, Func<Task<ServiceAccountRoleOrganisationDetails>> next, CancellationToken cancellationToken)
        {
            var serviceAccountRoleOrganisation = await _serviceAccountRoleOrganisationRepository.Get(request.ServiceAccountRoleOrganisationId);

            return serviceAccountRoleOrganisation;
        }
    }
}