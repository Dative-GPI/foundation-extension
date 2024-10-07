using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Clients.Admin.FoundationModels;
using System.Linq;

namespace Foundation.Extension.Admin.Handlers
{
    public class EntityPropertyTranslationsQueryHandler : IMiddleware<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyApplicationTranslation>>
    {
        private IEntityPropertyApplicationTranslationRepository _entityPropertyTranslationRepository;

        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly IRequestContextProvider _requestContextProvider;


        public EntityPropertyTranslationsQueryHandler(
            IEntityPropertyApplicationTranslationRepository entityPropertyTranslationRepository,
            IFoundationClientFactory foundationClientFactory,
            IRequestContextProvider requestContextProvider)
        {
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
            _foundationClientFactory = foundationClientFactory;
            _requestContextProvider = requestContextProvider;
        }

        public async Task<IEnumerable<EntityPropertyApplicationTranslation>> HandleAsync(EntityPropertyTranslationsQuery request, Func<Task<IEnumerable<EntityPropertyApplicationTranslation>>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var entityPropertyTranslations = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId,
                EntityPropertyId = request.EntityPropertyId,
            });

            var adminFoundationClient = await _foundationClientFactory.CreateAdmin(context.ApplicationId, context.LanguageCode);

            var entityPropertyTranslationsFoundation = await adminFoundationClient.Admin.EntityPropertyApplicationTranslations.GetMany(new EntityPropertyApplicationTranslationsFilterFoundationModel()
            {
                EntityPropertyIds = request.EntityPropertyIds,
            });


            return entityPropertyTranslations.Concat(entityPropertyTranslationsFoundation.Select(x => new EntityPropertyApplicationTranslation()
            {
                Id = x.Id,
                ApplicationId = context.ApplicationId,
                EntityPropertyId = x.EntityPropertyId,
                LanguageCode = x.LanguageCode,
                Label = x.Label,
                CategoryLabel = x.CategoryLabel,
            }));
        }
    }
}