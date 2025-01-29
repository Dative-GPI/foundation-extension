using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class PermissionOrganisationCategoriesQueryHandler : IMiddleware<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategoryInfos>>
    {
        private readonly IPermissionOrganisationCategoryRepository _permissionOrganisationCategoryRepository;
        
        public PermissionOrganisationCategoriesQueryHandler(IPermissionOrganisationCategoryRepository permissionOrganisationCategoryRepository)
        {
            _permissionOrganisationCategoryRepository = permissionOrganisationCategoryRepository;
        }

        public async Task<IEnumerable<PermissionOrganisationCategoryInfos>> HandleAsync(PermissionOrganisationCategoriesQuery request, Func<Task<IEnumerable<PermissionOrganisationCategoryInfos>>> next, CancellationToken cancellationToken)
        {
            var permissionOrganisationCategories = await _permissionOrganisationCategoryRepository.GetMany();

            return permissionOrganisationCategories;
        }
    }
}