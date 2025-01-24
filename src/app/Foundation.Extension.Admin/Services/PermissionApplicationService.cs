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
        private readonly IQueryHandler<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>> _permissionApplicationsQueryHandler;
        private readonly IPermissionProvider _permissionProvider;
        private readonly IMapper _mapper;

        public PermissionApplicationService
        (
            IQueryHandler<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>> permissionApplicationsQueryHandler,
            IPermissionProvider permissionProvider,
            IMapper mapper
        )
        {
            _permissionApplicationsQueryHandler = permissionApplicationsQueryHandler;
            _permissionProvider = permissionProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetCurrent()
        {
            return await _permissionProvider.GetPermissions();
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
    }
}