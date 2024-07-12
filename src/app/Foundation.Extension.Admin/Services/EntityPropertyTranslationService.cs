using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;
using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Admin.Services
{
    public class EntityPropertyTranslationService : IEntityPropertyTranslationService
    {
        private readonly IQueryHandler<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyTranslation>> _entityPropertyTranslationsQueryHandler;
        private readonly IQueryHandler<EntityPropertyTranslationsSpreadsheetQuery, byte[]> _entityPropertyTranslationsSpreadsheetQueryHandler;
        private readonly ICommandHandler<ReplaceEntityPropertyTranslationsCommand> _replaceEntityPropertyTranslationCommandHandler;
        private readonly ICommandHandler<UploadEntityPropertyTranslationsCommand> _uploadEntityPropertyTranslationsCommandHandler;
        private readonly IEntityPropertyTranslationRepository _entityPropertyTranslationRepository;

        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public EntityPropertyTranslationService(
            IQueryHandler<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyTranslation>> entityPropertyTranslationsQueryHandler,
            IQueryHandler<EntityPropertyTranslationsSpreadsheetQuery, byte[]> entityPropertyTranslationsSpreadsheetQueryHandler,
            ICommandHandler<ReplaceEntityPropertyTranslationsCommand> replaceEntityPropertyTranslationCommandHandler,
            ICommandHandler<UploadEntityPropertyTranslationsCommand> uploadEntityPropertyTranslationsCommandHandler,
            IEntityPropertyTranslationRepository entityPropertyTranslationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _entityPropertyTranslationsQueryHandler = entityPropertyTranslationsQueryHandler;
            _entityPropertyTranslationsSpreadsheetQueryHandler = entityPropertyTranslationsSpreadsheetQueryHandler;

            _replaceEntityPropertyTranslationCommandHandler = replaceEntityPropertyTranslationCommandHandler;
            _uploadEntityPropertyTranslationsCommandHandler = uploadEntityPropertyTranslationsCommandHandler;
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntityPropertyTranslationViewModel>> GetMany(EntityPropertyTranslationsFilterViewModel filter)
        {
            var query = new EntityPropertyTranslationsQuery()
            {
                EntityPropertyId = filter.EntityPropertyId,
                EntityPropertyIds = filter.EntityPropertyIds,
            };

            var result = await _entityPropertyTranslationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<EntityPropertyTranslation>, IEnumerable<EntityPropertyTranslationViewModel>>(result);
        }

        public async Task<IEnumerable<EntityPropertyTranslationViewModel>> Replace(Guid entityPropertyId, List<UpdateEntityPropertyTranslationViewModel> payload)
        {
            var context = _requestContextProvider.Context;
            var command = new ReplaceEntityPropertyTranslationsCommand()
            {
                ActorId = context.ActorId,
                ApplicationId = context.ApplicationId,

                EntityPropertyId = entityPropertyId,
                Translations = payload.Select(t => new ReplaceEntityPropertyTranslation()
                {
                    LanguageCode = t.LanguageCode,
                    Label = t.Label,
                    CategoryLabel = t.CategoryLabel
                }).ToList()
            };

            await _replaceEntityPropertyTranslationCommandHandler.HandleAsync(command);

            var result = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyTranslationsFilter()
            {
                EntityPropertyId = entityPropertyId
            });

            return _mapper.Map<IEnumerable<EntityPropertyTranslation>, IEnumerable<EntityPropertyTranslationViewModel>>(result);
        }

        public async Task<byte[]> Download()
        {
            var query = new EntityPropertyTranslationsSpreadsheetQuery()
            {
                ActorId = _requestContextProvider.Context.ActorId,
                ApplicationId = _requestContextProvider.Context.ApplicationId
            };

            return await _entityPropertyTranslationsSpreadsheetQueryHandler.HandleAsync(query);
        }

        public async Task<IEnumerable<EntityPropertyTranslationViewModel>> Upload(List<SpreadsheetColumnDefinitionViewModel> labels, List<SpreadsheetColumnDefinitionViewModel> categories, Stream file)
        {
            var command = new UploadEntityPropertyTranslationsCommand()
            {
                ActorId = _requestContextProvider.Context.ActorId,
                ApplicationId = _requestContextProvider.Context.ApplicationId,

                Labels = labels.Select(lc => new SpreadsheetColumnDefinition()
                {
                    Index = lc.Index,
                    LanguageCode = lc.LanguageCode
                }).ToList(),
                Categories = categories.Select(cc => new SpreadsheetColumnDefinition()
                {
                    Index = cc.Index,
                    LanguageCode = cc.LanguageCode
                }).ToList(),

                File = file
            };

            await _uploadEntityPropertyTranslationsCommandHandler.HandleAsync(command);

            var result = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyTranslationsFilter()
            {
                ApplicationId = _requestContextProvider.Context.ApplicationId
            });

            return _mapper.Map<IEnumerable<EntityPropertyTranslation>, IEnumerable<EntityPropertyTranslationViewModel>>(result);
        }
    }
}