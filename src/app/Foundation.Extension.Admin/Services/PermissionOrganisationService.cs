using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Services
{
    public class PermissionOrganisationService : IPermissionOrganisationService
    {
        private readonly IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> _permissionsQueryHandler;
        private readonly IMapper _mapper;

        public PermissionOrganisationService
        (
            IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>> permissionsQueryHandler,
            IMapper mapper
        )
        {
            _permissionsQueryHandler = permissionsQueryHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionOrganisationInfosViewModel>> GetMany(PermissionOrganisationsFilterViewModel filter)
        {
            var query = new PermissionOrganisationsQuery()
            {
                Search = filter.Search
            };

            var result = await _permissionsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionOrganisationInfos>, IEnumerable<PermissionOrganisationInfosViewModel>>(result);
        }
    }
}