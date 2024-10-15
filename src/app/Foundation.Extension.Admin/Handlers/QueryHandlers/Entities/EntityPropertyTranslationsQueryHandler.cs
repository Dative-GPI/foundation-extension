using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Clients.Admin.FoundationModels;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class EntityPropertyTranslationsQueryHandler : IMiddleware<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyApplicationTranslation>>
    {
        private readonly IEntityPropertyApplicationTranslationRepository _entityPropertyTranslationRepository;
        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly IRequestContextProvider _requestContextProvider;

        public EntityPropertyTranslationsQueryHandler
        (
            IRequestContextProvider requestContextProvider,
            IFoundationClientFactory foundationClientFactory,
            IEntityPropertyApplicationTranslationRepository entityPropertyTranslationRepository
        )
        {
            _requestContextProvider = requestContextProvider;
            _foundationClientFactory = foundationClientFactory;
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
        }

        public async Task<IEnumerable<EntityPropertyApplicationTranslation>> HandleAsync(EntityPropertyTranslationsQuery request, Func<Task<IEnumerable<EntityPropertyApplicationTranslation>>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var adminFoundationClient = await _foundationClientFactory.CreateAdmin(context.ApplicationId, context.LanguageCode);

            var entityPropertyTranslations = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId,
                EntityPropertyId = request.EntityPropertyId,
            });

            var entityPropertyTranslationsFoundation = await adminFoundationClient.Admin.EntityPropertyApplicationTranslations.GetMany(new EntityPropertyApplicationTranslationsFilterFoundationModel()
            {
                EntityPropertyIds = request.EntityPropertiesIds,
            });

            return entityPropertyTranslations.Concat(entityPropertyTranslationsFoundation.Select(x => new EntityPropertyApplicationTranslation()
            {
                Id = x.Id,
                ApplicationId = context.ApplicationId,
                EntityPropertyId = x.EntityPropertyId,
                LanguageCode = x.LanguageCode,
                Label = x.Label
            }));
        }
    }
}