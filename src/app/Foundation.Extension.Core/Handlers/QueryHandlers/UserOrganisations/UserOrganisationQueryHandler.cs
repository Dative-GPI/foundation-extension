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
    public class UserOrganisationQueryHandler : IMiddleware<UserOrganisationQuery, UserOrganisationDetails>
    {
		private readonly IFoundationClientFactory _foundationClientFactory;
		private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
        private readonly IRoleOrganisationRepository _roleOrganisationRepository;
        private readonly IRoleOrganisationTypeRepository _roleOrganisationTypeRepository;
		private readonly IRequestContextProvider _requestContextProvider;

        public UserOrganisationQueryHandler
        (
            IFoundationClientFactory foundationClientFactory,
            IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
            IRoleOrganisationRepository roleOrganisationRepository,
            IRoleOrganisationTypeRepository roleOrganisationTypeRepository,
            IRequestContextProvider requestContextProvider
        )
        {
            _foundationClientFactory = foundationClientFactory;
            _permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
            _roleOrganisationRepository = roleOrganisationRepository;
            _roleOrganisationTypeRepository = roleOrganisationTypeRepository;
            _requestContextProvider = requestContextProvider;
        }

        public async Task<UserOrganisationDetails> HandleAsync(UserOrganisationQuery request, Func<Task<UserOrganisationDetails>> next, CancellationToken cancellationToken)
        {
			var context = _requestContextProvider.Context;
			var organisationId = context.OrganisationId.Value;

			var client = await _foundationClientFactory.CreateAuthenticated(context.ApplicationId, context.LanguageCode, context.Jwt);

            var userOrganisation = await client.Core.UserOrganisations.Get(request.UserOrganisationId, organisationId);
			var organisation = await client.Gateway.Organisations.Get(organisationId);

            if (userOrganisation == null || (!userOrganisation.RoleId.HasValue && userOrganisation.UserId != organisation.AdminId))
            {
                return new UserOrganisationDetails()
                {
                    Id = request.UserOrganisationId,
                    Permissions = new List<PermissionItem>()
                };
            }

            var permissionOrganisationTypesFilter = new PermissionOrganisationTypesFilter()
			{
				OrganisationTypeId = organisation.OrganisationTypeId
			};
            
			var permissionOrganisationTypes = await _permissionOrganisationTypeRepository.GetMany(permissionOrganisationTypesFilter);

            if (userOrganisation.UserId == organisation.AdminId)
            {
                return new UserOrganisationDetails()
                {
                    Id = request.UserOrganisationId,
                    Permissions = permissionOrganisationTypes.Select(pot => new PermissionItem()
                    {
                        Id = pot.PermissionId,
                        Code = pot.PermissionCode
                    }).ToList()
                };
            }

			var permissions = new List<PermissionItem>();

            switch (userOrganisation.RoleType) {
                case Clients.Core.FoundationModels.RoleType.Organisation: {
                    var roleOrganisation = await _roleOrganisationRepository.Get(userOrganisation.RoleId.Value);
                    permissions = roleOrganisation.Permissions
                        .Where(p => permissionOrganisationTypes.Select(pot => pot.PermissionCode).Contains(p.Code))
                        .ToList();
                    break;
                }
                case Clients.Core.FoundationModels.RoleType.OrganisationType: {
                    var roleOrganisationType = await _roleOrganisationTypeRepository.Get(userOrganisation.RoleId.Value);
                    permissions = roleOrganisationType.Permissions
                        .Where(p => permissionOrganisationTypes.Select(pot => pot.PermissionCode).Contains(p.Code))
                        .ToList();
                    break;
                }
            }

			return new UserOrganisationDetails()
            {
                Id = request.UserOrganisationId,
                Permissions = permissions
            };
        }
    }
}