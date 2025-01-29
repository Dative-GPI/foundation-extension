using System;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;
using Foundation.Extension.Domain.Models;

using static Foundation.Extension.Core.AutoMapper.Consts;

namespace Foundation.Extension.Core.Services
{
    public class UserOrganisationService : IUserOrganisationService
    {
        private readonly IQueryHandler<CurrentUserOrganisationQuery, UserOrganisationDetails> _currentUserOrganisationQueryHandler;
        private readonly IQueryHandler<UserOrganisationQuery, UserOrganisationDetails> _userOrganisationQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public UserOrganisationService
        (
            IQueryHandler<CurrentUserOrganisationQuery, UserOrganisationDetails> currentUserOrganisationQueryHandler,
            IQueryHandler<UserOrganisationQuery, UserOrganisationDetails> userOrganisationQueryHandler,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _currentUserOrganisationQueryHandler = currentUserOrganisationQueryHandler;
            _userOrganisationQueryHandler = userOrganisationQueryHandler;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<UserOrganisationDetailsViewModel> GetCurrent()
        {
            var query = new CurrentUserOrganisationQuery();

            var result = await _currentUserOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<UserOrganisationDetails, UserOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }

        public async Task<UserOrganisationDetailsViewModel> Get(Guid userOrganisationId)
        {
            var query = new UserOrganisationQuery()
            {
                UserOrganisationId = userOrganisationId
            };

            var result = await _userOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<UserOrganisationDetails, UserOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}