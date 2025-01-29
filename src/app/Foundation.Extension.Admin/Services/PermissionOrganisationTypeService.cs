using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Admin.Services
{
    public class PermissionOrganisationTypeService : IPermissionOrganisationTypeService
    {
		private readonly IQueryHandler<PermissionOrganisationTypesQuery, IEnumerable<PermissionOrganisationTypeInfos>> _permissionOrganisationTypesQueryHandler;
        private readonly ICommandHandler<UpsertPermissionOrganisationTypesCommand> _updatePermissionOrganisationTypesCommand;
		private readonly IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;
        private readonly IMapper _mapper;

        public PermissionOrganisationTypeService
        (
            IQueryHandler<PermissionOrganisationTypesQuery, IEnumerable<PermissionOrganisationTypeInfos>> permissionOrganisationTypesQuery,
            ICommandHandler<UpsertPermissionOrganisationTypesCommand> updatePermissionOrganisationTypesCommand,
			IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository,
            IMapper mapper
        )
        {
            _permissionOrganisationTypesQueryHandler = permissionOrganisationTypesQuery;
            _updatePermissionOrganisationTypesCommand = updatePermissionOrganisationTypesCommand;
			_permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionOrganisationTypeInfosViewModel>> GetMany(PermissionOrganisationTypesFilterViewModel filter)
        {
            var query = new PermissionOrganisationTypesQuery()
            {
                OrganisationTypeId = filter.OrganisationTypeId
            };

            var result = await _permissionOrganisationTypesQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<PermissionOrganisationTypeInfos>, IEnumerable<PermissionOrganisationTypeInfosViewModel>>(result);
        }

        public async Task<IEnumerable<PermissionOrganisationTypeInfosViewModel>> Upsert(List<UpsertPermissionOrganisationTypesViewModel> payload)
        {
            var command = new UpsertPermissionOrganisationTypesCommand()
            {
                PermissionOrganisationTypes = payload.Select(tr => new UpsertPermissionOrganisationTypes()
                {
                    OrganisationTypeId = tr.OrganisationTypeId,
                    PermissionIds = tr.PermissionIds
                }).ToList()
            };

            await _updatePermissionOrganisationTypesCommand.HandleAsync(command);

            var filter = new PermissionOrganisationTypesFilter()
            {
				OrganisationTypeIds = payload.Select(x => x.OrganisationTypeId).Distinct().ToList()
            };

			var result = await _permissionOrganisationTypeRepository.GetMany(filter);

			return _mapper.Map<IEnumerable<PermissionOrganisationTypeInfos>, IEnumerable<PermissionOrganisationTypeInfosViewModel>>(result);
        }
    }
}