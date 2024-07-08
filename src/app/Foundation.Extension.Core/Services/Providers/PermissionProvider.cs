using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation.Clients.Abstractions;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Abstractions;


namespace Foundation.Extension.Core.Tools
{
  public class PermissionProvider : IPermissionProvider
  {
    private readonly IFoundationClientFactory _foundationClientFactory;
    private readonly IRolePermissionOrganisationRepository _roleOrganisationRepository;
    private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
    private readonly IRequestContextProvider _requestContextProvider;

    public PermissionProvider(
        IFoundationClientFactory foundationClientFactory,
        IRolePermissionOrganisationRepository roleOrganisationRepository,
        IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
        IRequestContextProvider requestContextProvider
    )
    {
      _foundationClientFactory = foundationClientFactory;
      _roleOrganisationRepository = roleOrganisationRepository;
      _permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
      _requestContextProvider = requestContextProvider;
    }


    public async Task<bool> HasPermissions(params string[] permissions)
    {
      var grantedPermissions = await GetPermissions();
      return !permissions.Except(grantedPermissions).Any(); // Checking if permissions is a subset of grantedPermissions
                                                            // Code from https://stackoverflow.com/a/333034
                                                            // Interesting conversation under this comment : https://stackoverflow.com/a/26697119
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
        return foundationPermissions.Concat(permissionOrganisationTypes).ToList();

      var userOrganisation = await GetUserOrganisation(client, organisationId, context.ActorId);
      if (userOrganisation == default || !userOrganisation.RoleId.HasValue)
        return foundationPermissions;

      var rolePermissionOrganisations = await GetRolePermissionOrganisations(userOrganisation.RoleId.Value);

      return foundationPermissions.Concat(
          rolePermissionOrganisations.Intersect(permissionOrganisationTypes).ToList()
      ).ToList();
      // use of intersect to make sure that the permissions of a role is a subset of
      // the permissions of an organisationType 
    }

    private async Task<IEnumerable<string>> GetFoundationPermissions(IFoundationClient client, Guid organisationId)
    {
      var permissions = await client.Core.Permissions.GetCurrent(organisationId.ToString());
      return permissions.Select(permission => permission.Code).ToList();
    }

    private async Task<IEnumerable<string>> GetPermissionOrganisationTypes(Guid organisationTypeId)
    {
      var permissionOrganisationTypes = await _permissionOrganisationTypeRepository.GetMany(
          new PermissionOrganisationTypesFilter()
          {
            OrganisationTypeId = organisationTypeId
          }
      );

      return permissionOrganisationTypes.Select(otp => otp.PermissionCode).ToList();
    }

    private async Task<IEnumerable<string>> GetRolePermissionOrganisations(Guid roleId)
    {
      var role = await _roleOrganisationRepository.Get(roleId);

      return role.Permissions.Select(rp => rp.Code).ToList();
    }

    private async Task<Clients.Core.FoundationModels.UserOrganisationInfosFoundationModel> GetUserOrganisation(IFoundationClient client, Guid organisationId, Guid userId)
    {
      var userOrganisations = await client.Core.UserOrganisations.GetMany(
          organisationId.ToString(),
          new Clients.Core.FoundationModels.UserOrganisationsFilterFoundationModel()
          {
          }
      );
      return userOrganisations.FirstOrDefault();
    }
  }
}