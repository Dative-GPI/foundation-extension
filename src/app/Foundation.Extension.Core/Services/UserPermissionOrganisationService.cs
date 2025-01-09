using System;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;
using Foundation.Extension.Domain.Models;


using static Foundation.Extension.Core.AutoMapper.Consts;

namespace Foundation.Extension.Core.Services
{
    public class UserPermissionOrganisationService : IUserPermissionOrganisationService
    {
        private readonly IQueryHandler<UserPermissionOrganisationQuery, UserPermissionOrganisationDetails> _userPermissionOrganisationQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public UserPermissionOrganisationService
        (
            IQueryHandler<UserPermissionOrganisationQuery, UserPermissionOrganisationDetails> userPermissionOrganisationQueryHandler,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _userPermissionOrganisationQueryHandler = userPermissionOrganisationQueryHandler;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<UserPermissionOrganisationDetailsViewModel> GetUserPermissionOrganisation(Guid userId, UserType userType)
        {
            var query = new UserPermissionOrganisationQuery()
            {
                UserId = userId,
                UserType = userType
            };

            var result = await _userPermissionOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<UserPermissionOrganisationDetails, UserPermissionOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}