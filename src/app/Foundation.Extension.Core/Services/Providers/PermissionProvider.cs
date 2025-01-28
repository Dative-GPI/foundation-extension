using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation.Clients.Abstractions;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.Core.Tools
{
	public class PermissionProvider : IPermissionProvider
	{
		private readonly IFoundationClientFactory _foundationClientFactory;
		private readonly IRoleOrganisationRepository _roleOrganisationRepository;
		private readonly IRoleOrganisationTypeRepository _roleOrganisationTypeRepository;
		private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
		private readonly IRequestContextProvider _requestContextProvider;

		public PermissionProvider
		(
			IFoundationClientFactory foundationClientFactory,
			IRoleOrganisationRepository roleOrganisationRepository,
			IRoleOrganisationTypeRepository roleOrganisationTypeRepository,
			IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
			IRequestContextProvider requestContextProvider
		)
		{
			_foundationClientFactory = foundationClientFactory;
			_roleOrganisationRepository = roleOrganisationRepository;
			_roleOrganisationTypeRepository = roleOrganisationTypeRepository;
			_permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
			_requestContextProvider = requestContextProvider;
		}


		public async Task<bool> HasPermissions(params string[] permissions)
		{
			// Checking if permissions is a subset of grantedPermissions
			// Code from https://stackoverflow.com/a/333034
			// Interesting conversation under this comment : https://stackoverflow.com/a/26697119
			var grantedPermissions = await GetPermissions();
			return !permissions.Except(grantedPermissions).Any(); 
		}


		public async Task<IEnumerable<string>> GetPermissions()
		{
			var context = _requestContextProvider.Context;
			var organisationId = context.OrganisationId.Value;

			var client = await _foundationClientFactory.CreateAuthenticated(context.ApplicationId, context.LanguageCode, context.Jwt);
			var foundationPermissions = await GetFoundationPermissions(client, organisationId);

			var organisation = await client.Gateway.Organisations.Get(organisationId);
			var permissionOrganisationTypes = await GetPermissionOrganisationTypes(organisation.OrganisationTypeId);

			if (organisation.AdminId == context.ActorId)
			{
				return foundationPermissions.Concat(permissionOrganisationTypes).ToList();
			}

			var userOrganisation = await client.Core.UserOrganisations.GetCurrent(organisationId);
			if (userOrganisation == default || !userOrganisation.RoleId.HasValue)
			{
				return foundationPermissions;
			}

			var permissions = new List<string>();

            switch (userOrganisation.RoleType) {
                case Clients.Core.FoundationModels.RoleType.Organisation: {
                    permissions = await GetRoleOrganisationPermissions(userOrganisation.RoleId.Value);
                    break;
                }
                case Clients.Core.FoundationModels.RoleType.OrganisationType: {
                    permissions = await GetRoleOrganisationTypePermissions(userOrganisation.RoleId.Value);
                    break;
                }
            }

			// Use of intersect to make sure that the permissions of a role is a subset of
			// the permissions of an organisation type 
			return foundationPermissions.Concat(
				permissions.Intersect(permissionOrganisationTypes).ToList()
			).ToList();
		}

		private static async Task<IEnumerable<string>> GetFoundationPermissions(IFoundationClient client, Guid organisationId)
		{
			var permissions = await client.Core.Permissions.GetCurrent(organisationId);
			return permissions.Select(permission => permission.Code).ToList();
		}

		private async Task<IEnumerable<string>> GetPermissionOrganisationTypes(Guid organisationTypeId)
		{
			var filter = new PermissionOrganisationTypesFilter()
			{
				OrganisationTypeId = organisationTypeId
			};

			var permissionOrganisationTypes = await _permissionOrganisationTypeRepository.GetMany(filter);

			return permissionOrganisationTypes.Select(otp => otp.PermissionCode).ToList();
		}

		private async Task<List<string>> GetRoleOrganisationPermissions(Guid roleOrganisationId)
		{
			var roleOrganisation = await _roleOrganisationRepository.Get(roleOrganisationId);

			return roleOrganisation.Permissions.Select(rp => rp.Code).ToList();
		}

		private async Task<List<string>> GetRoleOrganisationTypePermissions(Guid roleOrganisationTypeId)
		{
			var roleOrganisationType = await _roleOrganisationTypeRepository.Get(roleOrganisationTypeId);

			return roleOrganisationType.Permissions.Select(rp => rp.Code).ToList();
		}
	}
}