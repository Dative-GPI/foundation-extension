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
    public class PermissionApplicationCategoriesQueryHandler : IMiddleware<PermissionApplicationCategoriesQuery, IEnumerable<PermissionApplicationCategory>>
    {
        private IPermissionApplicationCategoryRepository _permissionApplicationCategoryRepository;
        
        public PermissionApplicationCategoriesQueryHandler(IPermissionApplicationCategoryRepository permissionApplicationCategoryRepository)
        {
            _permissionApplicationCategoryRepository = permissionApplicationCategoryRepository;
        }

        public async Task<IEnumerable<PermissionApplicationCategory>> HandleAsync(PermissionApplicationCategoriesQuery request, Func<Task<IEnumerable<PermissionApplicationCategory>>> next, CancellationToken cancellationToken)
        {
            return await _permissionApplicationCategoryRepository.GetMany();
        }
    }
}