using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Bones.Flow;

using Foundation.Template.Domain.Models;
using Foundation.Template.Core.Abstractions;
using Foundation.Template.Core.ViewModels;

using static Foundation.Template.Core.AutoMapper.Consts;

namespace Foundation.Template.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionProvider _permissionProvider;
        private readonly IQueryHandler<PermissionCategoriesQuery, IEnumerable<PermissionCategory>> _categoriesQueryHandler;
        private readonly IQueryHandler<PermissionsQuery, IEnumerable<PermissionInfos>> _permissionsQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public PermissionService(
            IQueryHandler<PermissionsQuery, IEnumerable<PermissionInfos>> permissionsQueryHandler,
            IQueryHandler<PermissionCategoriesQuery, IEnumerable<PermissionCategory>> categoriesQueryHandler,
            IRequestContextProvider requestContextProvider,
            IPermissionProvider permissionProvider,
            IMapper mapper
        )
        {
            _categoriesQueryHandler = categoriesQueryHandler;
            _permissionsQueryHandler = permissionsQueryHandler;

            _requestContextProvider = requestContextProvider;
            _permissionProvider = permissionProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetCurrent()
        {
            return await _permissionProvider.GetPermissions();
        }

        public async Task<IEnumerable<PermissionCategoryViewModel>> GetCategories()
        {
            var query = new PermissionCategoriesQuery();

            var categories = await _categoriesQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<IEnumerable<PermissionCategory>, IEnumerable<PermissionCategoryViewModel>>(categories, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<IEnumerable<PermissionInfosViewModel>> GetMany()
        {
            var query = new PermissionsQuery();

            var result = await _permissionsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionInfos>, IEnumerable<PermissionInfosViewModel>>(result);
        }
    }
}