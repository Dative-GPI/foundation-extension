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
        private readonly IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails> _rolePermissionOrganisationQueryHandler;
        private readonly ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>> _updateRolePermissionOrganisationCommandHandler;
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public RolePermissionOrganisationService
        (
            IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails> rolePermissionOrganisationQueryHandler,
            ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>> updateRolePermissionOrganisationCommandHandler,
            IRolePermissionOrganisationRepository rolePermissionOrganisationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _rolePermissionOrganisationQueryHandler = rolePermissionOrganisationQueryHandler;
            _updateRolePermissionOrganisationCommandHandler = updateRolePermissionOrganisationCommandHandler;
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> GetRolePermissionOrganisation(Guid roleId)
        {
            var query = new RolePermissionOrganisationQuery()
            {
                RoleId = roleId
            };

            var result = await _rolePermissionOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<RolePermissionOrganisationDetailsViewModel> UpdateRolePermissionOrganisation(Guid roleId, UpdateRolePermissionOrganisationViewModel payload)
        {
            var command = new UpdateRolePermissionOrganisationCommand()
            {
                RoleId = roleId,
                PermissionIds = payload.PermissionIds
            };

            var entity = await _updateRolePermissionOrganisationCommandHandler.HandleAsync(command);
            var result = await _rolePermissionOrganisationRepository.Get(entity.Id);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}