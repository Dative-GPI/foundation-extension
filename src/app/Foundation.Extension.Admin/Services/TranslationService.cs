using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Admin.Requests;

namespace Foundation.Extension.Admin.Services
{
    public class TranslationService : ITranslationService
    {
        private IQueryHandler<TranslationsQuery, IEnumerable<Translation>> _translationsQueryHandler;
        private IMapper _mapper;

        public TranslationService(
            IQueryHandler<TranslationsQuery, IEnumerable<Translation>> translationsQueryHandler,
            IMapper mapper
        )
        {
            _translationsQueryHandler = translationsQueryHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TranslationViewModel>> GetMany()
        {
            var query = new TranslationsQuery();

            var result = await _translationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<Translation>, IEnumerable<TranslationViewModel>>(result);
        }
    }
}