using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RolePermissionOrganisationService : IRolePermissionOrganisationService
    {
        private readonly IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails> _roleOrganisationQueryHandler;
        private readonly ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>> _updateRolePermissionOrganisationCommandHandler;
        private readonly IRolePermissionOrganisationRepository _roleOrganisationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public RolePermissionOrganisationService(
            IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails> roleOrganisationQueryHandler,
            ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>> updateRolePermissionOrganisationCommandHandler,
            IRequestContextProvider requestContextProvider,
            IRolePermissionOrganisationRepository roleOrganisationRepository,
            IMapper mapper
        )
        {
            _roleOrganisationQueryHandler = roleOrganisationQueryHandler;
            _updateRolePermissionOrganisationCommandHandler = updateRolePermissionOrganisationCommandHandler;

            _roleOrganisationRepository = roleOrganisationRepository;

            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> Get(Guid roleId)
        {
            var query = new RolePermissionOrganisationQuery() {
                RoleOrganisationId = roleId
            };

            var result = await _roleOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> Update(Guid roleId, UpdateRolePermissionOrganisationViewModel payload)
        {
            var command = new UpdateRolePermissionOrganisationCommand() {
                RoleOrganisationId = roleId,
                PermissionIds = payload.PermissionIds
            };

            var entity = await _updateRolePermissionOrganisationCommandHandler.HandleAsync(command);
            var result = await _roleOrganisationRepository.Get(entity.Id);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}