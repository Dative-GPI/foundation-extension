using System;
using System.Collections.Generic;
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
    public class RolePermissionOrganisationService : IRolePermissionOrganisationService
    {
        private IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails> _roleOrganisationQueryHandler;
        private ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>> _updateRolePermissionOrganisationCommandHandler;
        private IRolePermissionOrganisationRepository _roleOrganisationRepository;
        private IMapper _mapper;

        public RolePermissionOrganisationService(
            IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails> roleOrganisationQueryHandler,
            ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>> updateRolePermissionOrganisationCommandHandler,
            IRolePermissionOrganisationRepository roleOrganisationRepository,
            IMapper mapper
        )
        {
            _roleOrganisationQueryHandler = roleOrganisationQueryHandler;
            _updateRolePermissionOrganisationCommandHandler = updateRolePermissionOrganisationCommandHandler;
            _roleOrganisationRepository = roleOrganisationRepository;
            _mapper = mapper;
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> Get(Guid id)
        {
            var query = new RolePermissionOrganisationQuery()
            {
                RoleOrganisationId = id
            };

            var result = await _roleOrganisationQueryHandler.HandleAsync(query);

            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result);
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> Update(Guid id, UpdateRolePermissionOrganisationViewModel payload)
        {
            var command = new UpdateRolePermissionOrganisationCommand()
            {
                RoleOrganisationId = id,
                PermissionIds = payload.PermissionIds,
                ExtensionData = payload.ExtensionData
            };

            var entity = await _updateRolePermissionOrganisationCommandHandler.HandleAsync(command);
            var result = await _roleOrganisationRepository.Get(entity.Id);

            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result);
        }
    }
}