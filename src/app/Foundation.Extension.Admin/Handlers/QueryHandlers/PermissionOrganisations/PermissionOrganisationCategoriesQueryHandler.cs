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
    public class PermissionOrganisationCategoriesQueryHandler : IMiddleware<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>
    {
        private IPermissionOrganisationCategoryRepository _permissionOrganisationCategoryRepository;
        
        public PermissionOrganisationCategoriesQueryHandler(IPermissionOrganisationCategoryRepository permissionOrganisationCategoryRepository)
        {
            _permissionOrganisationCategoryRepository = permissionOrganisationCategoryRepository;
        }

        public async Task<IEnumerable<PermissionOrganisationCategory>> HandleAsync(PermissionOrganisationCategoriesQuery request, Func<Task<IEnumerable<PermissionOrganisationCategory>>> next, CancellationToken cancellationToken)
        {
            return await _permissionOrganisationCategoryRepository.GetMany();
        }
    }
}