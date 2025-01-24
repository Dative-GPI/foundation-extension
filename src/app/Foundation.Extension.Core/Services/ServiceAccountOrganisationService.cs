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
    public class ServiceAccountOrganisationService : IServiceAccountOrganisationService
    {
        private readonly IQueryHandler<ServiceAccountOrganisationQuery, ServiceAccountOrganisationDetails> _userOrganisationQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public ServiceAccountOrganisationService
        (
            IQueryHandler<ServiceAccountOrganisationQuery, ServiceAccountOrganisationDetails> userOrganisationQueryHandler,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _userOrganisationQueryHandler = userOrganisationQueryHandler;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<ServiceAccountOrganisationDetailsViewModel> Get(Guid userOrganisationId)
        {
            var query = new ServiceAccountOrganisationQuery()
            {
                ServiceAccountOrganisationId = userOrganisationId
            };

            var result = await _userOrganisationQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<ServiceAccountOrganisationDetails, ServiceAccountOrganisationDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}