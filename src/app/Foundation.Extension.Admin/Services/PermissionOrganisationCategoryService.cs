using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Services
{
    public class PermissionOrganisationCategoryService : IPermissionOrganisationCategoryService
    {
        private readonly IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>> _permissionCategoriesQueryHandler;
        private readonly IMapper _mapper;

        public PermissionOrganisationCategoryService
        (
            IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>> permissionCategoriesQueryHandler,
            IMapper mapper
        )
        {
            _permissionCategoriesQueryHandler = permissionCategoriesQueryHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionOrganisationCategoryViewModel>> GetMany()
        {
            var query = new PermissionOrganisationCategoriesQuery();

            var result = await _permissionCategoriesQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionOrganisationCategory>, IEnumerable<PermissionOrganisationCategoryViewModel>>(result);
        }
    }
}