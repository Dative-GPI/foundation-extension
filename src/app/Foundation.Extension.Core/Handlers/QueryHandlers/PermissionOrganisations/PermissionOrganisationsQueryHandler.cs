using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Handlers
{
    public class PermissionOrganisationsQueryHandler : IMiddleware<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>
    {
        private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
        private readonly IPermissionOrganisationRepository _permissionOrganisationRepository;
        private readonly IRequestContextProvider _requestContextProvider;

        public PermissionOrganisationsQueryHandler
        (
            IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
            IPermissionOrganisationRepository permissionOrganisationRepository,
            IRequestContextProvider requestContextProvider
        )
        {
            _permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
            _permissionOrganisationRepository = permissionOrganisationRepository;
            
            _requestContextProvider = requestContextProvider;
        }

        public async Task<IEnumerable<PermissionOrganisationInfos>> HandleAsync(PermissionOrganisationsQuery request, Func<Task<IEnumerable<PermissionOrganisationInfos>>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var permissionOrganisationTypes = await _permissionOrganisationTypeRepository.GetMany(new PermissionOrganisationTypesFilter()
            {
                OrganisationTypeId = context.OrganisationTypeId
            });

            var permissionOrganisations = await _permissionOrganisationRepository.GetMany(new PermissionsFilter()
            {
                PermissionIds = permissionOrganisationTypes.Select(p => p.PermissionId).ToList()
            });

            return permissionOrganisations;
        }
    }
}