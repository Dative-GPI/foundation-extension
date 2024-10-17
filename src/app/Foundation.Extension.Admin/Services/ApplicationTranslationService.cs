using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Admin.Services
{
    public class ApplicationTranslationService : IApplicationTranslationService
    {
        private readonly IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>> _applicationTranslationsQueryHandler;
        private readonly IQueryHandler<ApplicationTranslationsSpreadsheetQuery, byte[]> _applicationTranslationsSpreadsheetQueryHandler;
        private readonly ICommandHandler<UpdateApplicationTranslationCommand> _updateApplicationTranslationsCommandHandler;
        private readonly ICommandHandler<UploadApplicationTranslationsCommand> _uploadApplicationTranslationsCommandHandler;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IMapper _mapper;

        public ApplicationTranslationService
        (
            IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>> applicationTranslationsQueryHandler,
            IQueryHandler<ApplicationTranslationsSpreadsheetQuery, byte[]> applicationTranslationsSpreadsheetQueryHandler,
            ICommandHandler<UpdateApplicationTranslationCommand> updateApplicationTranslationsCommandHandler,
            ICommandHandler<UploadApplicationTranslationsCommand> uploadApplicationTranslationsCommandHandler,
            IApplicationTranslationRepository applicationTranslationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _applicationTranslationsQueryHandler = applicationTranslationsQueryHandler;
            _applicationTranslationsSpreadsheetQueryHandler = applicationTranslationsSpreadsheetQueryHandler;
            _updateApplicationTranslationsCommandHandler = updateApplicationTranslationsCommandHandler;
            _uploadApplicationTranslationsCommandHandler = uploadApplicationTranslationsCommandHandler;
            _applicationTranslationRepository = applicationTranslationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationTranslationViewModel>> GetMany(ApplicationTranslationViewModel filter)
        {
            var query = new ApplicationTranslationsQuery()
            {
                LanguageCode = filter.LanguageCode,
                TranslationCode = filter.TranslationCode
            };

            var result = await _applicationTranslationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(result);
        }

        public async Task<IEnumerable<ApplicationTranslationViewModel>> Update(string code, UpdateApplicationTranslationViewModel payload)
        {
            var context = _requestContextProvider.Context;
            var command = new UpdateApplicationTranslationCommand()
            {
                Code = code,
                Translations = payload.Translations.Select(t => new UpdateApplicationTranslationLanguageCommand()
                {
                    LanguageCode = t.LanguageCode,
                    Value = t.Value
                }).ToList()
            };

            await _updateApplicationTranslationsCommandHandler.HandleAsync(command);
            var result = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId,
                Codes = new List<string>() { code }
            });

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(result);
        }

        public async Task<byte[]> Download()
        {
            var context = _requestContextProvider.Context;
            var query = new ApplicationTranslationsSpreadsheetQuery()
            {
                ApplicationId = context.ApplicationId
            };

            return await _applicationTranslationsSpreadsheetQueryHandler.HandleAsync(query);
        }

        public async Task<IEnumerable<ApplicationTranslationViewModel>> Upload(IEnumerable<SpreadsheetColumnDefinitionViewModel> languages, Stream file)
        {
            var command = new UploadApplicationTranslationsCommand()
            {
                ApplicationId = _requestContextProvider.Context.ApplicationId,

                Languages = languages.Select(lc => new SpreadsheetColumnDefinition()
                {
                    Index = lc.Index,
                    LanguageCode = lc.LanguageCode
                }),
                File = file
            };

            await _uploadApplicationTranslationsCommandHandler.HandleAsync(command);
            
            var result = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = _requestContextProvider.Context.ApplicationId
            });

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(result);
        }
    }
}