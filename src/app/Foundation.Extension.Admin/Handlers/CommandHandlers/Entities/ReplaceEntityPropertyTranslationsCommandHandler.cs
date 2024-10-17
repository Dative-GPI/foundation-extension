using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class ReplaceEntityPropertyTranslationsCommandHandler : IMiddleware<ReplaceEntityPropertyTranslationsCommand>
    {
        private readonly IEntityPropertyRepository _entityPropertyRepository;
        private readonly IEntityPropertyApplicationTranslationRepository _entityPropertyTranslationRepository;

        public ReplaceEntityPropertyTranslationsCommandHandler
        (
            IEntityPropertyRepository entityPropertyRepository,
            IEntityPropertyApplicationTranslationRepository entityPropertyTranslationRepository
        )
        {
            _entityPropertyRepository = entityPropertyRepository;
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
        }

        public async Task HandleAsync(ReplaceEntityPropertyTranslationsCommand command, Func<Task> next, CancellationToken cancellationToken)
        {
            var property = await _entityPropertyRepository.Get(command.EntityPropertyId);

            if (property == default)
            {
                throw new Exception(ErrorCode.EntityNotFound);
            }

            var formerEntityProperties = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                ApplicationId = command.ApplicationId,
                EntityPropertyId = command.EntityPropertyId
            });

            await _entityPropertyTranslationRepository.RemoveRange(formerEntityProperties.Select(t => t.Id));

            var newEntityProperties = command.Translations.Select(t => new CreateEntityPropertyApplicationTranslation()
            {
                ApplicationId = command.ApplicationId,
                LanguageCode = t.LanguageCode,
                EntityPropertyId = property.Id,
                Label = t.Label
            }).ToList();

            await _entityPropertyTranslationRepository.CreateRange(newEntityProperties);
        }
    }
}
