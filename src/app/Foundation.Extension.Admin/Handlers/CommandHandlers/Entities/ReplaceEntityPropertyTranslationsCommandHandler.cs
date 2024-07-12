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
        private readonly IEntityPropertyTranslationRepository _entityPropertyTranslationRepository;

        public ReplaceEntityPropertyTranslationsCommandHandler
        (
            IEntityPropertyRepository entityPropertyRepository,
            IEntityPropertyTranslationRepository entityPropertyTranslationRepository
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

            var formerEntityPropertys = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyTranslationsFilter()
            {
                ApplicationId = command.ApplicationId,
                EntityPropertyId = command.EntityPropertyId
            });

            await _entityPropertyTranslationRepository.RemoveRange(formerEntityPropertys.Select(t => t.Id));

            var newEntityPropertys = command.Translations.Select(t => new CreateEntityPropertyTranslation()
            {
                ApplicationId = command.ApplicationId,
                LanguageCode = t.LanguageCode,
                EntityPropertyId = property.Id,

                Label = t.Label,
                CategoryLabel = t.CategoryLabel
            }).ToList();

            await _entityPropertyTranslationRepository.CreateRange(newEntityPropertys);
        }
    }
}
