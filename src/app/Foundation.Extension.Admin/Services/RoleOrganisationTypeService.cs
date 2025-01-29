using System;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Services
{
    public class RoleOrganisationTypeService : IRoleOrganisationTypeService
    {
        private readonly IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails> _roleOrganisationTypeQueryHandler;
        private readonly ICommandHandler<UpdateRoleOrganisationTypeCommand, IEntity<Guid>> _updateRoleOrganisationTypeCommandHandler;
        private readonly IRoleOrganisationTypeRepository _roleOrganisationTypeRepository;
        private readonly IMapper _mapper;

        public RoleOrganisationTypeService
        (
            IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails> roleOrganisationTypeQueryHandler,
            ICommandHandler<UpdateRoleOrganisationTypeCommand, IEntity<Guid>> updateRoleOrganisationTypeCommandHandler,
            IRoleOrganisationTypeRepository roleOrganisationTypeRepository,
            IMapper mapper
        )
        {
            _roleOrganisationTypeQueryHandler = roleOrganisationTypeQueryHandler;
            _updateRoleOrganisationTypeCommandHandler = updateRoleOrganisationTypeCommandHandler;
            _roleOrganisationTypeRepository = roleOrganisationTypeRepository;
            _mapper = mapper;
        }

        public async Task<RoleOrganisationTypeDetailsViewModel> Get(Guid roleOrganisationTypeId)
        {
            var query = new RoleOrganisationTypeQuery()
            {
                RoleOrganisationTypeId = roleOrganisationTypeId
            };

            var result = await _roleOrganisationTypeQueryHandler.HandleAsync(query);

            return _mapper.Map<RoleOrganisationTypeDetails, RoleOrganisationTypeDetailsViewModel>(result);
        }

        public async Task<RoleOrganisationTypeDetailsViewModel> Update(Guid roleOrganisationTypeId, UpdateRoleOrganisationTypeViewModel payload)
        {
            var command = new UpdateRoleOrganisationTypeCommand()
            {
                RoleOrganisationTypeId = roleOrganisationTypeId,
                PermissionIds = payload.PermissionIds,
                ExtensionData = payload.ExtensionData
            };

            var entity = await _updateRoleOrganisationTypeCommandHandler.HandleAsync(command);
            var result = await _roleOrganisationTypeRepository.Get(entity.Id);

            return _mapper.Map<RoleOrganisationTypeDetails, RoleOrganisationTypeDetailsViewModel>(result);
        }
    }
}