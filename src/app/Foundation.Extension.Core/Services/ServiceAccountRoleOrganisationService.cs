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
    public class ServiceAccountRoleOrganisationService : IServiceAccountRoleOrganisationService
    {
        private readonly IQueryHandler<ServiceAccountRoleOrganisationQuery, ServiceAccountRoleOrganisationDetails> _serviceAccountRoleOrganisationQueryHandler;
        private readonly ICommandHandler<UpdateServiceAccountRoleOrganisationCommand, IEntity<Guid>> _updateServiceAccountRoleOrganisationCommandHandler;
        private readonly IRolePermissionOrganisationRepository _rolePermissionOrganisationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public ServiceAccountRoleOrganisationService
        (
            IQueryHandler<ServiceAccountRoleOrganisationQuery, ServiceAccountRoleOrganisationDetails> serviceAccountRoleOrganisationQueryHandler,
            ICommandHandler<UpdateServiceAccountRoleOrganisationCommand, IEntity<Guid>> updateServiceAccountRoleOrganisationCommandHandler,
            IRolePermissionOrganisationRepository rolePermissionOrganisationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _serviceAccountRoleOrganisationQueryHandler = serviceAccountRoleOrganisationQueryHandler;
            _updateServiceAccountRoleOrganisationCommandHandler = updateServiceAccountRoleOrganisationCommandHandler;
            _rolePermissionOrganisationRepository = rolePermissionOrganisationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<ServiceAccountRoleOrganisationDetailsViewModel> Get(Guid serviceAccountRoleOrganisationId)
        {
            var query = new ServiceAccountRoleOrganisationQuery()
            {
                ServiceAccountRoleOrganisationId = serviceAccountRoleOrganisationId
            };

            var result = await _serviceAccountRoleOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<ServiceAccountRoleOrganisationDetails, ServiceAccountRoleOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<ServiceAccountRoleOrganisationDetailsViewModel> Update(Guid serviceAccountRoleOrganisationId, UpdateServiceAccountRoleOrganisationViewModel payload)
        {
            var command = new UpdateServiceAccountRoleOrganisationCommand()
            {
                ServiceAccountRoleOrganisationId = serviceAccountRoleOrganisationId,
                PermissionIds = payload.PermissionIds,
                ExtensionData = payload.ExtensionData
            };

            var entity = await _updateServiceAccountRoleOrganisationCommandHandler.HandleAsync(command);
            var baseRole = await _rolePermissionOrganisationRepository.Get(entity.Id);

            var result = new ServiceAccountRoleOrganisationDetails()
            {
                Id = serviceAccountRoleOrganisationId,
                Permissions = baseRole.Permissions
            };

            var context = _requestContextProvider.Context;
            return _mapper.Map<ServiceAccountRoleOrganisationDetails, ServiceAccountRoleOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}