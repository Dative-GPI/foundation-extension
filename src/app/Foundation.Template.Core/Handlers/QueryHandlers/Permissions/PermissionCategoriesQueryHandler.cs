using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Template.Domain.Models;
using Foundation.Template.Domain.Repositories.Interfaces;

namespace Foundation.Template.Core.Handlers {
    public class PermissionCategoriesQueryHandler : IMiddleware<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>
    {
        private readonly IPermissionOrganisationCategoryRepository _permissionOrganisationCategoryRepository;

        public PermissionCategoriesQueryHandler(
            IPermissionOrganisationCategoryRepository permissionOrganisationCategoryRepository
        )
        {
            _permissionOrganisationCategoryRepository = permissionOrganisationCategoryRepository;
        }

        public async Task<IEnumerable<PermissionOrganisationCategory>> HandleAsync(PermissionOrganisationCategoriesQuery request, Func<Task<IEnumerable<PermissionOrganisationCategory>>> next, CancellationToken cancellationToken)
        {
            var categories = await _permissionOrganisationCategoryRepository.GetMany();
            return categories;
        }
    }
}