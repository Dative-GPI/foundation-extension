using System.Collections.Generic;
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
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>> _permissionOrganisationCategoriesQueryHandler;
        private readonly IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> _permissionOrganisationsQueryHandler;
        private readonly IPermissionProvider _permissionProvider;
        private readonly IMapper _mapper;

        public PermissionOrganisationService
        (
            IRequestContextProvider requestContextProvider,
            IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>> permissionOrganisationCategoriesQueryHandler,
            IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> permissionOrganisationsQueryHandler,
            IPermissionProvider permissionProvider,
            IMapper mapper
        )
        {
            _requestContextProvider = requestContextProvider;
            _permissionOrganisationCategoriesQueryHandler = permissionOrganisationCategoriesQueryHandler;
            _permissionOrganisationsQueryHandler = permissionOrganisationsQueryHandler;
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

            var categories = await _permissionOrganisationCategoriesQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<IEnumerable<PermissionOrganisationCategory>, IEnumerable<PermissionOrganisationCategoryViewModel>>(categories, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<IEnumerable<PermissionOrganisationInfosViewModel>> GetMany()
        {
            var query = new PermissionOrganisationsQuery();

            var result = await _permissionOrganisationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionOrganisationInfos>, IEnumerable<PermissionOrganisationInfosViewModel>>(result);
        }
    }
}