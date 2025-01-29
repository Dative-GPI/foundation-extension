using System;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

using static Foundation.Extension.Core.AutoMapper.Consts;

namespace Foundation.Extension.Core.Services
{
    public class RoleOrganisationTypeService : IRoleOrganisationTypeService
    {
        private readonly IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails> _roleOrganisationTypeQueryHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public RoleOrganisationTypeService
        (
            IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails> roleOrganisationTypeQueryHandler,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _roleOrganisationTypeQueryHandler = roleOrganisationTypeQueryHandler;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<RoleOrganisationTypeDetailsViewModel> Get(Guid roleOrganisationTypeId)
        {
            var query = new RoleOrganisationTypeQuery()
            {
                RoleOrganisationTypeId = roleOrganisationTypeId
            };

            var result = await _roleOrganisationTypeQueryHandler.HandleAsync(query);

            var context = _requestContextProvider.Context;
            return _mapper.Map<RoleOrganisationTypeDetails, RoleOrganisationTypeDetailsViewModel>(result, opt => opt.Items.Add(LANGUAGE, context.LanguageCode));
        }
    }
}