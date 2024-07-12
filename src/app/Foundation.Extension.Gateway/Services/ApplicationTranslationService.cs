using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Services
{
    public class ApplicationTranslationService : IApplicationTranslationService
    {
        private IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>> _applicationTranslationsQueryHandler;
        private IMapper _mapper;

        public ApplicationTranslationService(
            IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>> applicationTranslationsQueryHandler,
            IMapper mapper
        )
        {
            _applicationTranslationsQueryHandler = applicationTranslationsQueryHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationTranslationViewModel>> GetMany()
        {
            var query = new ApplicationTranslationsQuery();

            var result = await _applicationTranslationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(result);
        }
    }
}