using System;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

using static Foundation.Extension.Core.AutoMapper.Consts;

namespace Foundation.Extension.Core.Services
{
    public class RoleOrganisationService : IRoleOrganisationService
    {
        private readonly IQueryHandler<RoleOrganisationQuery, RoleOrganisationDetails> _roleOrganisationQueryHandler;
        private readonly ICommandHandler<UpdateRoleOrganisationCommand, IEntity<Guid>> _updateRoleOrganisationCommandHandler;
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public RoleOrganisationService
        (
            IQueryHandler<RoleOrganisationQuery, RoleOrganisationDetails> roleOrganisationQueryHandler,
            ICommandHandler<UpdateRoleOrganisationCommand, IEntity<Guid>> updateRoleOrganisationCommandHandler,
            IRolePermissionOrganisationRepository rolePermissionOrganisationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _roleOrganisationQueryHandler = roleOrganisationQueryHandler;
            _updateRoleOrganisationCommandHandler = updateRoleOrganisationCommandHandler;
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<RoleOrganisationDetailsViewModel> Get(Guid roleOrganisationId)
        {
            var query = new RoleOrganisationQuery()
            {
                RoleOrganisationId = roleOrganisationId
            };

            var result = await _roleOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RoleOrganisationDetails, RoleOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RoleOrganisationDetailsViewModel> Update(Guid roleOrganisationId, UpdateRoleOrganisationViewModel payload)
        {
            var command = new UpdateRoleOrganisationCommand()
            {
                RoleOrganisationId = roleOrganisationId,
                PermissionIds = payload.PermissionIds,
                ExtensionData = payload.ExtensionData
            };

            var entity = await _updateRoleOrganisationCommandHandler.HandleAsync(command);
            var baseRole = await _rolePermissionOrganisationRepository.Get(entity.Id);

            var result = new RoleOrganisationDetails()
            {
                Id = roleOrganisationId,
                Permissions = baseRole.Permissions
            };

            var context = _requestContextProvider.Context;
            return _mapper.Map<RoleOrganisationDetails, RoleOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}