using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

using static Foundation.Extension.Core.AutoMapper.Consts;

namespace Foundation.Extension.Core.Services
{
    public class PermissionOrganisationService : IPermissionOrganisationService
    {
        private readonly IPermissionProvider _permissionProvider;
        private readonly IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>> _categoriesQueryHandler;
        private readonly IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> _permissionsQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public PermissionOrganisationService(
            IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> permissionsQueryHandler,
            IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>> categoriesQueryHandler,
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

        public async Task<IEnumerable<PermissionOrganisationCategoryViewModel>> GetCategories()
        {
            var query = new PermissionOrganisationCategoriesQuery();

            var categories = await _categoriesQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<IEnumerable<PermissionOrganisationCategory>, IEnumerable<PermissionOrganisationCategoryViewModel>>(categories, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<IEnumerable<PermissionOrganisationInfosViewModel>> GetMany()
        {
            var query = new PermissionOrganisationsQuery();

            var result = await _permissionsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionOrganisationInfos>, IEnumerable<PermissionOrganisationInfosViewModel>>(result);
        }
    }
}