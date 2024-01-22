using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Template.Admin.Abstractions;
using Foundation.Template.Admin.Requests;
using Foundation.Template.Domain.Models;
using Foundation.Template.Domain.Repositories.Commands;
using Foundation.Template.Domain.Repositories.Filters;
using Foundation.Template.Domain.Repositories.Interfaces;

namespace Foundation.Template.Admin.Handlers
{
    public class UpdateApplicationTranslationsCommandHandler : IMiddleware<UpdateApplicationTranslationCommand>
    {
        private IRequestContextProvider _requestContextProvider;
        private ITranslationRepository _translationRepository;
        private IApplicationTranslationRepository _applicationTranslationRepository;

        public UpdateApplicationTranslationsCommandHandler(
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