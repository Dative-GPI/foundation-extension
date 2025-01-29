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
        private readonly IQueryHandler<CurrentPermissionOrganisationsQuery, IEnumerable<string>> _currentPermissionOrganisationsQueryHandler;
        private readonly IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> _permissionOrganisationsQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public PermissionOrganisationService
        (
            IQueryHandler<CurrentPermissionOrganisationsQuery, IEnumerable<string>> currentPermissionOrganisationsQueryHandler,
            IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> permissionOrganisationsQueryHandler,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _currentPermissionOrganisationsQueryHandler = currentPermissionOrganisationsQueryHandler;
            _permissionOrganisationsQueryHandler = permissionOrganisationsQueryHandler;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetCurrent()
        {
            var query = new CurrentPermissionOrganisationsQuery();

            var result = await _currentPermissionOrganisationsQueryHandler.HandleAsync(query);

            return result;
        }

        public async Task<IEnumerable<PermissionOrganisationInfosViewModel>> GetMany()
        {
            var query = new PermissionOrganisationsQuery();

            var result = await _permissionOrganisationsQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<IEnumerable<PermissionOrganisationInfos>, IEnumerable<PermissionOrganisationInfosViewModel>>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}