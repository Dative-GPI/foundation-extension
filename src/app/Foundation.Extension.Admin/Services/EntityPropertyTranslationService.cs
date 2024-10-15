using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Services
{
    public class EntityPropertyTranslationService : IEntityPropertyTranslationService
    {
        private readonly IQueryHandler<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyApplicationTranslation>> _entityPropertyTranslationsQueryHandler;
        private readonly IQueryHandler<EntityPropertyTranslationsSpreadsheetQuery, byte[]> _entityPropertyTranslationsSpreadsheetQueryHandler;
        private readonly ICommandHandler<ReplaceEntityPropertyTranslationsCommand> _replaceEntityPropertyTranslationCommandHandler;
        private readonly ICommandHandler<UploadEntityPropertyTranslationsCommand> _uploadEntityPropertyTranslationsCommandHandler;
        private readonly IEntityPropertyApplicationTranslationRepository _entityPropertyTranslationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public EntityPropertyTranslationService
        (
            IQueryHandler<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyApplicationTranslation>> entityPropertyTranslationsQueryHandler,
            IQueryHandler<EntityPropertyTranslationsSpreadsheetQuery, byte[]> entityPropertyTranslationsSpreadsheetQueryHandler,
            ICommandHandler<ReplaceEntityPropertyTranslationsCommand> replaceEntityPropertyTranslationCommandHandler,
            ICommandHandler<UploadEntityPropertyTranslationsCommand> uploadEntityPropertyTranslationsCommandHandler,
            IEntityPropertyApplicationTranslationRepository entityPropertyTranslationRepository,
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
                EntityPropertiesIds = filter.EntityPropertiesIds,
            };

            var result = await _entityPropertyTranslationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<EntityPropertyApplicationTranslation>, IEnumerable<EntityPropertyTranslationViewModel>>(result);
        }

        public async Task<IEnumerable<EntityPropertyTranslationViewModel>> Replace(Guid entityPropertyId, List<UpdateEntityPropertyTranslationViewModel> payload)
        {
            var context = _requestContextProvider.Context;
            var command = new ReplaceEntityPropertyTranslationsCommand()
            {
                ApplicationId = context.ApplicationId,

                EntityPropertyId = entityPropertyId,
                Translations = payload.Select(t => new ReplaceEntityPropertyTranslation()
                {
                    LanguageCode = t.LanguageCode,
                    Label = t.Label
                }).ToList()
            };

            await _replaceEntityPropertyTranslationCommandHandler.HandleAsync(command);

            var result = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                EntityPropertyId = entityPropertyId
            });

            return _mapper.Map<IEnumerable<EntityPropertyApplicationTranslation>, IEnumerable<EntityPropertyTranslationViewModel>>(result);
        }

        public async Task<byte[]> Download()
        {
            var query = new EntityPropertyTranslationsSpreadsheetQuery()
            {
                ApplicationId = _requestContextProvider.Context.ApplicationId
            };

            return await _entityPropertyTranslationsSpreadsheetQueryHandler.HandleAsync(query);
        }

        public async Task<IEnumerable<EntityPropertyTranslationViewModel>> Upload(List<SpreadsheetColumnDefinitionViewModel> languages, Stream file)
        {
            var command = new UploadEntityPropertyTranslationsCommand()
            {
                ApplicationId = _requestContextProvider.Context.ApplicationId,

                Languages = languages.Select(lc => new SpreadsheetColumnDefinition()
                {
                    Index = lc.Index,
                    LanguageCode = lc.LanguageCode
                }).ToList(),

                File = file
            };

            await _uploadEntityPropertyTranslationsCommandHandler.HandleAsync(command);

            var result = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                ApplicationId = _requestContextProvider.Context.ApplicationId
            });

            return _mapper.Map<IEnumerable<EntityPropertyApplicationTranslation>, IEnumerable<EntityPropertyTranslationViewModel>>(result);
        }
    }
}