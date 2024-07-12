using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Services
{
    public class PermissionApplicationService : IPermissionApplicationService
    {
        private IQueryHandler<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>> _permissionApplicationsQueryHandler;
        private IRequestContextProvider _requestContextProvider;
        private IPermissionProvider _permissionProvider;
        private IMapper _mapper;

        public PermissionApplicationService(
            IQueryHandler<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>> permissionApplicationsQueryHandler,
            IRequestContextProvider requestContextProvider,
            IPermissionProvider permissionProvider,
            IMapper mapper
        )
        {
            _permissionApplicationsQueryHandler = permissionApplicationsQueryHandler;
            _requestContextProvider = requestContextProvider;
            _permissionProvider = permissionProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionApplicationInfosViewModel>> GetMany(PermissionApplicationFilterViewModel filter)
        {
            var query = new PermissionApplicationsQuery()
            {
                Search = filter.Search
            };

            var result = await _permissionApplicationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionApplicationInfos>, IEnumerable<PermissionApplicationInfosViewModel>>(result);
        }

        public async Task<IEnumerable<string>> GetCurrent()
        {
            return await _permissionProvider.GetPermissions();
        }
    }
}