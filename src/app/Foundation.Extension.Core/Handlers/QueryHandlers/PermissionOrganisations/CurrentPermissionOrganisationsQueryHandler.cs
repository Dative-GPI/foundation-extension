using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Handlers
{
    public class CurrentPermissionOrganisationsQueryHandler : IMiddleware<CurrentPermissionOrganisationsQuery, IEnumerable<string>>
    {
        private readonly IPermissionProvider _permissionProvider;

        public CurrentPermissionOrganisationsQueryHandler(IPermissionProvider permissionProvider)
        {
            _permissionProvider = permissionProvider;
        }

        public async Task<IEnumerable<string>> HandleAsync(CurrentPermissionOrganisationsQuery request, Func<Task<IEnumerable<string>>> next, CancellationToken cancellationToken)
        {
            var permissionOrganisations = await _permissionProvider.GetPermissions();

            return permissionOrganisations;
        }
    }
}