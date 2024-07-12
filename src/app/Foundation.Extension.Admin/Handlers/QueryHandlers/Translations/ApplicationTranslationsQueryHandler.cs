using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class ApplicationTranslationsQueryHandler : IMiddleware<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>>
    {
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;

        public ApplicationTranslationsQueryHandler(
            IRequestContextProvider requestContextProvider,
            IApplicationTranslationRepository applicationTranslationRepository
        )
        {
            _requestContextProvider = requestContextProvider;
            _applicationTranslationRepository = applicationTranslationRepository;
        }

        public async Task<IEnumerable<ApplicationTranslation>> HandleAsync(ApplicationTranslationsQuery request, Func<Task<IEnumerable<ApplicationTranslation>>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var filter = new ApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId,
                LanguageCode = request.LanguageCode,
                TranslationCode = request.TranslationCode
            };

            var result = await _applicationTranslationRepository.GetMany(filter);

            return result;
        }
    }
}