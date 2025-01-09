using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Handlers
{
    public class UserPermissionOrganisationQueryHandler : IMiddleware<UserPermissionOrganisationQuery, UserPermissionOrganisationDetails>
    {
		private readonly IFoundationClientFactory _foundationClientFactory;
		private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;
		private readonly IRequestContextProvider _requestContextProvider;

        public UserPermissionOrganisationQueryHandler
        (
            IFoundationClientFactory foundationClientFactory,
            IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
            IRolePermissionOrganisationRepository rolePermissionOrganisationRepository,
            IRequestContextProvider requestContextProvider
        )
        {
            _foundationClientFactory = foundationClientFactory;
            _permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
            _requestContextProvider = requestContextProvider;
        }

        public async Task<UserPermissionOrganisationDetails> HandleAsync(UserPermissionOrganisationQuery request, Func<Task<UserPermissionOrganisationDetails>> next, CancellationToken cancellationToken)
        {
			var context = _requestContextProvider.Context;
			var organisationId = context.OrganisationId.Value;

			var client = await _foundationClientFactory.CreateAuthenticated(context.ApplicationId, context.LanguageCode, context.Jwt);

			var organisation = await client.Gateway.Organisations.Get(organisationId);

            var permissionOrganisationTypesFilter = new PermissionOrganisationTypesFilter()
			{
				OrganisationTypeId = organisation.OrganisationTypeId
			};
            
			var permissionOrganisationTypes = await _permissionOrganisationTypeRepository.GetMany(permissionOrganisationTypesFilter);

            // request.UserId is either a UserOrganisationId or ServiceAccountOrganisationId
            Guid? userId = null;
            Guid? roleId = null;
            switch (request.UserType)
            {
                case UserType.User:
                    var userOrganisation = await client.Core.UserOrganisations.Get(request.UserId, organisationId);
                    userId = userOrganisation.UserId;
                    roleId = userOrganisation.RoleId;
                    break;
                case UserType.ServiceAccount:
                    var serviceAccountOrganisation = await client.Core.ServiceAccountOrganisations.Get(request.UserId, organisationId);
                    userId = serviceAccountOrganisation.UserId;
                    roleId = serviceAccountOrganisation.RoleId;
                    break;
            }

            if (!userId.HasValue || (!roleId.HasValue && userId.Value != organisation.AdminId))
            {
                return new UserPermissionOrganisationDetails()
                {
                    Id = request.UserId,
                    Permissions = new List<PermissionItem>()
                };
            }
            if (userId == organisation.AdminId)
            {
                return new UserPermissionOrganisationDetails()
                {
                    Id = request.UserId,
                    Permissions = permissionOrganisationTypes.Select(pot => new PermissionItem()
                    {
                        Id = pot.PermissionId,
                        Code = pot.PermissionCode
                    }).ToList()
                };
            }

			var rolePermissionOrganisations = await _rolePermissionOrganisationRepository.Get(roleId.Value);

			return new UserPermissionOrganisationDetails()
            {
                Id = request.UserId,
                Permissions = rolePermissionOrganisations.Permissions
                    .Where(p => permissionOrganisationTypes.Select(pot => pot.PermissionCode).Contains(p.Code))
                    .ToList()
            };
        }
    }
}