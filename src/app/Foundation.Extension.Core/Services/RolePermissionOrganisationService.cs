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
    public class RolePermissionOrganisationService : IRolePermissionOrganisationService
    {
        private readonly IQueryHandler<ServiceAccountRoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails> _serviceAccountRoleOrganisationPermissionOrganisationQueryHandler;
        private readonly IQueryHandler<RoleOrganisationTypePermissionOrganisationQuery, RolePermissionOrganisationDetails> _roleOrganisationTypePermissionOrganisationQueryHandler;
        private readonly IQueryHandler<RoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails> _roleOrganisationPermissionOrganisationQueryHandler;
        private readonly ICommandHandler<UpdateServiceAccountRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>> _updateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler;
        private readonly ICommandHandler<UpdateRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>> _updateRoleOrganisationPermissionOrganisationCommandHandler;
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public RolePermissionOrganisationService
        (
            IQueryHandler<ServiceAccountRoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails> serviceAccountRoleOrganisationPermissionOrganisationQueryHandler,
            IQueryHandler<RoleOrganisationTypePermissionOrganisationQuery, RolePermissionOrganisationDetails> roleOrganisationTypePermissionOrganisationQueryHandler,
            IQueryHandler<RoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails> roleOrganisationPermissionOrganisationQueryHandler,
            ICommandHandler<UpdateServiceAccountRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>> updateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler,
            ICommandHandler<UpdateRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>> updateRoleOrganisationPermissionOrganisationCommandHandler,
            IRolePermissionOrganisationRepository rolePermissionOrganisationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _serviceAccountRoleOrganisationPermissionOrganisationQueryHandler = serviceAccountRoleOrganisationPermissionOrganisationQueryHandler;
            _roleOrganisationTypePermissionOrganisationQueryHandler = roleOrganisationTypePermissionOrganisationQueryHandler;
            _roleOrganisationPermissionOrganisationQueryHandler = roleOrganisationPermissionOrganisationQueryHandler;
            _updateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler = updateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler;
            _updateRoleOrganisationPermissionOrganisationCommandHandler = updateRoleOrganisationPermissionOrganisationCommandHandler;
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> GetServiceAccountRoleOrganisation(Guid roleId)
        {
            var query = new ServiceAccountRoleOrganisationPermissionOrganisationQuery()
            {
                RoleId = roleId
            };

            var result = await _serviceAccountRoleOrganisationPermissionOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> GetRoleOrganisationType(Guid roleId)
        {
            var query = new RoleOrganisationTypePermissionOrganisationQuery()
            {
                RoleId = roleId
            };

            var result = await _roleOrganisationTypePermissionOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> GetRoleOrganisation(Guid roleId)
        {
            var query = new RoleOrganisationPermissionOrganisationQuery()
            {
                RoleId = roleId
            };

            var result = await _roleOrganisationPermissionOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> UpdateServiceAccountRoleOrganisation(Guid roleId, UpdateRolePermissionOrganisationViewModel payload)
        {
            var command = new UpdateServiceAccountRoleOrganisationPermissionOrganisationCommand()
            {
                RoleId = roleId,
                PermissionIds = payload.PermissionIds
            };

            var entity = await _updateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler.HandleAsync(command);
            var result = await _rolePermissionOrganisationRepository.Get(entity.Id);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> UpdateRoleOrganisation(Guid roleId, UpdateRolePermissionOrganisationViewModel payload)
        {
            var command = new UpdateRoleOrganisationPermissionOrganisationCommand()
            {
                RoleId = roleId,
                PermissionIds = payload.PermissionIds
            };

            var entity = await _updateRoleOrganisationPermissionOrganisationCommandHandler.HandleAsync(command);
            var result = await _rolePermissionOrganisationRepository.Get(entity.Id);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}