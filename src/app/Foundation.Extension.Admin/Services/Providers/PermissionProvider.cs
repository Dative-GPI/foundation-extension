using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation.Clients.Abstractions;

using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Abstractions;

using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.Providers
{
    public class PermissionProvider : IPermissionProvider
    {
        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly IRoleApplicationRepository _roleApplicationRepository;
        private readonly IRequestContextProvider _requestContextProvider;

        public PermissionProvider(
            IFoundationClientFactory foundationClientFactory,
            IRoleApplicationRepository roleApplicationRepository,
            IRequestContextProvider requestContextProvider
        )
        {
            _foundationClientFactory = foundationClientFactory;
            _roleApplicationRepository = roleApplicationRepository;
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
            List<string> permissions = new List<string>();
            var context = _requestContextProvider.Context;

            var client = await _foundationClientFactory.CreateAuthenticated(context.ApplicationId, context.LanguageCode, context.Jwt);

            var foundationPermissions = await client.Admin.PermissionsAdmin.GetCurrent();

            var localPermissions = await GetLocalPermissions(client);

            return foundationPermissions.Select(fp => fp.Code).Union(localPermissions).ToList();
        }
        private async Task<IEnumerable<string>> GetLocalPermissions(IFoundationClient client)
        {
            var userApplication = await client.Admin.Users.GetCurrent();

            if (userApplication.RoleAdminId.HasValue)
            {
                var role = await _roleApplicationRepository.Get(userApplication.RoleAdminId.Value);

                return role.Permissions.Select(p => p.Code);
            }

            return Enumerable.Empty<string>();
        }
    }
}