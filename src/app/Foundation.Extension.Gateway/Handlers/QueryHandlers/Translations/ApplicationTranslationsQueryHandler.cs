using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Gateway.Abstractions;

namespace Foundation.Extension.Gateway.Handlers
{
    public class ApplicationTranslationsQueryHandler : IMiddleware<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>>
    {
        private readonly ITranslationsProvider _translationProvider;
        private readonly IRequestContextProvider _requestContextProvider;

        public ApplicationTranslationsQueryHandler(
            ITranslationsProvider translationProvider,
            IRequestContextProvider requestContextProvider
        )
        {
            _translationProvider = translationProvider;
            _requestContextProvider = requestContextProvider;
        }

        public async Task<IEnumerable<ApplicationTranslation>> HandleAsync(ApplicationTranslationsQuery request, Func<Task<IEnumerable<ApplicationTranslation>>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            return await _translationProvider.GetMany(
                context.ApplicationId,
                context.LanguageCode
            );
        }
    }
}