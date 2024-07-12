using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class PermissionOrganisationsQueryHandler : IMiddleware<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>
    {
        private IPermissionOrganisationRepository _permissionRepository;
        
        public PermissionOrganisationsQueryHandler(IPermissionOrganisationRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<PermissionOrganisationInfos>> HandleAsync(PermissionOrganisationsQuery request, Func<Task<IEnumerable<PermissionOrganisationInfos>>> next, CancellationToken cancellationToken)
        {
            var filter = new PermissionsFilter()
            {
                Search = request.Search
            };

            return await _permissionRepository.GetMany(filter);
        }
    }
}