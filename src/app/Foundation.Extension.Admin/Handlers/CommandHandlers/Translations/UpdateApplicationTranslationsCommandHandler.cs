using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UpdateApplicationTranslationsCommandHandler : IMiddleware<UpdateApplicationTranslationCommand>
    {
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly ITranslationRepository _translationRepository;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;

        public UpdateApplicationTranslationsCommandHandler
        (
            IRequestContextProvider requestContextProvider,
            ITranslationRepository translationRepository,
            IApplicationTranslationRepository applicationTranslationRepository)
        {
            _requestContextProvider = requestContextProvider;
            _translationRepository = translationRepository;
            _applicationTranslationRepository = applicationTranslationRepository;
        }

        public async Task HandleAsync(UpdateApplicationTranslationCommand command, Func<Task> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;
            
            var defaultTranslation = (await _translationRepository.GetMany())
                 .FirstOrDefault(t => t.Code == command.Code);

            if (defaultTranslation == default)
            {
                throw new Exception(ErrorCode.EntityNotFound);
            }

            var formerTranslations = (await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId
            })).Where(at => at.TranslationCode == command.Code);

            await _applicationTranslationRepository.RemoveRange(formerTranslations.Select(t => t.Id));

            var newTranslations = command.Translations.Select(t => new CreateApplicationTranslation()
            {
                ApplicationId = context.ApplicationId,
                LanguageCode = t.LanguageCode,
                TranslationId = defaultTranslation.Id,
                Value = t.Value
            });

            await _applicationTranslationRepository.CreateRange(newTranslations);
        }
    }
}