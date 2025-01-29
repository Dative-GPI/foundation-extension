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
    public class PermissionOrganisationCategoryService : IPermissionOrganisationCategoryService
    {
        private readonly IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategoryInfos>> _permissionOrganisationCategoriesQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public PermissionOrganisationCategoryService
        (
            IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategoryInfos>> permissionOrganisationCategoriesQueryHandler,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _permissionOrganisationCategoriesQueryHandler = permissionOrganisationCategoriesQueryHandler;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionOrganisationCategoryInfosViewModel>> GetMany()
        {
            var query = new PermissionOrganisationCategoriesQuery();

            var result = await _permissionOrganisationCategoriesQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<IEnumerable<PermissionOrganisationCategoryInfos>, IEnumerable<PermissionOrganisationCategoryInfosViewModel>>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}