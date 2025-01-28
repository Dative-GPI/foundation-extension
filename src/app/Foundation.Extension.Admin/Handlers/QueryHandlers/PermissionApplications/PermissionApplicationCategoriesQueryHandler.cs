using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class PermissionApplicationCategoriesQueryHandler : IMiddleware<PermissionApplicationCategoriesQuery, IEnumerable<PermissionApplicationCategoryInfos>>
    {
        private readonly IPermissionApplicationCategoryRepository _permissionApplicationCategoryRepository;
        
        public PermissionApplicationCategoriesQueryHandler(IPermissionApplicationCategoryRepository permissionApplicationCategoryRepository)
        {
            _permissionApplicationCategoryRepository = permissionApplicationCategoryRepository;
        }

        public async Task<IEnumerable<PermissionApplicationCategoryInfos>> HandleAsync(PermissionApplicationCategoriesQuery request, Func<Task<IEnumerable<PermissionApplicationCategoryInfos>>> next, CancellationToken cancellationToken)
        {
            var permissionApplicationCategories = await _permissionApplicationCategoryRepository.GetMany();

            return permissionApplicationCategories;
        }
    }
}