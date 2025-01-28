using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Services
{
    public class PermissionApplicationCategoryService : IPermissionApplicationCategoryService
    {
        private readonly IQueryHandler<PermissionApplicationCategoriesQuery, IEnumerable<PermissionApplicationCategoryInfos>> _permissionApplicationCategoriesQueryHandler;
        private readonly IMapper _mapper;

        public PermissionApplicationCategoryService
        (
            IQueryHandler<PermissionApplicationCategoriesQuery, IEnumerable<PermissionApplicationCategoryInfos>> permissionApplicationCategoriesQueryHandler,
            IMapper mapper
        )
        {
            _permissionApplicationCategoriesQueryHandler = permissionApplicationCategoriesQueryHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionApplicationCategoryViewModel>> GetMany()
        {
            var query = new PermissionApplicationCategoriesQuery();

            var result = await _permissionApplicationCategoriesQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionApplicationCategoryInfos>, IEnumerable<PermissionApplicationCategoryViewModel>>(result);
        }
    }
}