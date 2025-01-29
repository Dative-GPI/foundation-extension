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
        private readonly IPermissionOrganisationRepository _permissionOrganisationRepository;
        
        public PermissionOrganisationsQueryHandler(IPermissionOrganisationRepository permissionOrganisationRepository)
        {
            _permissionOrganisationRepository = permissionOrganisationRepository;
        }

        public async Task<IEnumerable<PermissionOrganisationInfos>> HandleAsync(PermissionOrganisationsQuery request, Func<Task<IEnumerable<PermissionOrganisationInfos>>> next, CancellationToken cancellationToken)
        {
            var filter = new PermissionsFilter()
            {
                Search = request.Search
            };

            var permissionOrganisations = await _permissionOrganisationRepository.GetMany(filter);
        
            return permissionOrganisations;
        }
    }
}