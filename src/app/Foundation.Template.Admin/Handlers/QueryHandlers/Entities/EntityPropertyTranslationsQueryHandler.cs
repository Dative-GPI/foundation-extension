using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Template.Domain.Models;
using Foundation.Template.Domain.Repositories.Filters;
using Foundation.Template.Domain.Repositories.Interfaces;
using Foundation.Template.Domain.Abstractions;
using Foundation.Template.Admin.Abstractions;
using Foundation.Clients.Admin.FoundationModels;
using System.Linq;

namespace Foundation.Template.Admin.Handlers
{
    public class EntityPropertyTranslationsQueryHandler : IMiddleware<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyTranslation>>
    {
        private IEntityPropertyTranslationRepository _entityPropertyTranslationRepository;

        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly IRequestContextProvider _requestContextProvider;


        public EntityPropertyTranslationsQueryHandler(
            IEntityPropertyTranslationRepository entityPropertyTranslationRepository,
            IFoundationClientFactory foundationClientFactory,
            IRequestContextProvider requestContextProvider)
        {
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
            _foundationClientFactory = foundationClientFactory;
            _requestContextProvider = requestContextProvider;
        }

        public async Task<IEnumerable<EntityPropertyTranslation>> HandleAsync(EntityPropertyTranslationsQuery request, Func<Task<IEnumerable<EntityPropertyTranslation>>> next, CancellationToken cancellationToken)
        {
            var entityPropertyTranslations = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyTranslationsFilter()
            {
                ApplicationId = request.ApplicationId,
                EntityPropertyId = request.EntityPropertyId,
                // EntityPropertyIds = request.EntityPropertyIds,
            });


            var context = _requestContextProvider.Context;

            var adminFoundationClient = await _foundationClientFactory.CreateAdmin(context.ApplicationId, context.LanguageCode);

            var entityPropertyTranslationsFoundation = await adminFoundationClient.Admin.EntityPropertyTranslations.GetMany(new EntityPropertyTranslationsFilterFoundationModel()
            {
                EntityPropertyIds = request.EntityPropertyIds,
            });


            return entityPropertyTranslations.Concat(entityPropertyTranslationsFoundation.Select(x => new EntityPropertyTranslation()
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