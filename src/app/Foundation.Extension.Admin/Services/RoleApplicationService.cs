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
    public class RoleApplicationService : IRoleApplicationService
    {
        private readonly IQueryHandler<RoleApplicationQuery, RoleApplicationDetails> _roleApplicationQueryHandler;
        private readonly ICommandHandler<UpdateRoleApplicationCommand, IEntity<Guid>> _updateRoleApplicationCommandHandler;
        private readonly IRoleApplicationRepository _roleApplicationRepository;
        private readonly IMapper _mapper;

        public RoleApplicationService
        (
            IQueryHandler<RoleApplicationQuery, RoleApplicationDetails> roleQueryHandler,
            ICommandHandler<UpdateRoleApplicationCommand, IEntity<Guid>> updateRoleCommandHandler,
            IRoleApplicationRepository roleApplicationRepository,
            IMapper mapper
        )
        {
            _roleApplicationQueryHandler = roleQueryHandler;
            _updateRoleApplicationCommandHandler = updateRoleCommandHandler;
            _roleApplicationRepository = roleApplicationRepository;
            _mapper = mapper;
        }

        public async Task<RoleApplicationDetailsViewModel> Get(Guid roleApplicationId)
        {
            var query = new RoleApplicationQuery()
            {
                RoleApplicationId = roleApplicationId
            };

            var result = await _roleApplicationQueryHandler.HandleAsync(query);

            return _mapper.Map<RoleApplicationDetails, RoleApplicationDetailsViewModel>(result);
        }

        public async Task<RoleApplicationDetailsViewModel> Update(Guid roleApplicationId, UpdateRoleApplicationViewModel payload)
        {
            var command = new UpdateRoleApplicationCommand()
            {
                RoleApplicationId = roleApplicationId,
                PermissionIds = payload.PermissionIds,
                ExtensionData = payload.ExtensionData
            };

            var entity = await _updateRoleApplicationCommandHandler.HandleAsync(command);
            var result = await _roleApplicationRepository.Get(entity.Id);

            return _mapper.Map<RoleApplicationDetails, RoleApplicationDetailsViewModel>(result);
        }
    }
}