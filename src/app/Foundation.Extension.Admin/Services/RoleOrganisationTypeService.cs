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
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;
        private readonly IMapper _mapper;

        public RoleOrganisationTypeService
        (
            IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails> roleOrganisationTypeQueryHandler,
            ICommandHandler<UpdateRoleOrganisationTypeCommand, IEntity<Guid>> updateRoleOrganisationTypeCommandHandler,
            IRolePermissionOrganisationRepository rolePermissionOrganisationRepository,
            IMapper mapper
        )
        {
            _roleOrganisationTypeQueryHandler = roleOrganisationTypeQueryHandler;
            _updateRoleOrganisationTypeCommandHandler = updateRoleOrganisationTypeCommandHandler;
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
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
            var baseRole = await _rolePermissionOrganisationRepository.Get(entity.Id);

            var result = new RoleOrganisationTypeDetails()
            {
                Id = roleOrganisationTypeId,
                Permissions = baseRole.Permissions
            };

            return _mapper.Map<RoleOrganisationTypeDetails, RoleOrganisationTypeDetailsViewModel>(result);
        }
    }
}