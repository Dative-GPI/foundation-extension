using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class ServiceAccountOrganisationQueryHandler : IMiddleware<ServiceAccountOrganisationQuery, ServiceAccountOrganisationDetails>
    {
		private readonly IFoundationClientFactory _foundationClientFactory;
		private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
        private readonly IServiceAccountRoleOrganisationRepository _serviceAccountRoleOrganisationRepository;
		private readonly IRequestContextProvider _requestContextProvider;

        public ServiceAccountOrganisationQueryHandler
        (
            IFoundationClientFactory foundationClientFactory,
            IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
            IServiceAccountRoleOrganisationRepository serviceAccountRoleOrganisationRepository,
            IRequestContextProvider requestContextProvider
        )
        {
            _foundationClientFactory = foundationClientFactory;
            _permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
            _serviceAccountRoleOrganisationRepository = serviceAccountRoleOrganisationRepository;
            _requestContextProvider = requestContextProvider;
        }

        public async Task<ServiceAccountOrganisationDetails> HandleAsync(ServiceAccountOrganisationQuery request, Func<Task<ServiceAccountOrganisationDetails>> next, CancellationToken cancellationToken)
        {
			var context = _requestContextProvider.Context;
			var organisationId = context.OrganisationId.Value;

			var client = await _foundationClientFactory.CreateAuthenticated(context.ApplicationId, context.LanguageCode, context.Jwt);

            var serviceAccountOrganisation = await client.Core.ServiceAccountOrganisations.Get(request.ServiceAccountOrganisationId, organisationId);
			var organisation = await client.Gateway.Organisations.Get(organisationId);

            if (serviceAccountOrganisation == null || !serviceAccountOrganisation.RoleId.HasValue)
            {
                return new ServiceAccountOrganisationDetails()
                {
                    Id = request.ServiceAccountOrganisationId,
                    Permissions = new List<PermissionItem>()
                };
            }

            var permissionOrganisationTypesFilter = new PermissionOrganisationTypesFilter()
			{
				OrganisationTypeId = organisation.OrganisationTypeId
			};
            
			var permissionOrganisationTypes = await _permissionOrganisationTypeRepository.GetMany(permissionOrganisationTypesFilter);

            var serviceAccountRoleOrganisation = await _serviceAccountRoleOrganisationRepository.Get(serviceAccountOrganisation.RoleId.Value);

			return new ServiceAccountOrganisationDetails()
            {
                Id = request.ServiceAccountOrganisationId,
                Permissions = serviceAccountRoleOrganisation.Permissions
                    .Where(p => permissionOrganisationTypes.Select(pot => pot.PermissionCode).Contains(p.Code))
                    .ToList()
            };
        }
    }
}