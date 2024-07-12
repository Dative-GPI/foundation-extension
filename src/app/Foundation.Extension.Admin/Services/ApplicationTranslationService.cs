using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Admin.Requests;
using System.Linq;
using System.IO;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Admin.Services
{
    public class ApplicationTranslationService : IApplicationTranslationService
    {
        private IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>> _applicationTranslationsQueryHandler;
        private ICommandHandler<UpdateApplicationTranslationCommand> _updateApplicationTranslationsCommandHandler;
        private readonly ICommandHandler<DownloadApplicationTranslationsCommand> _downloadApplicationTranslationsCommandHandler;
        private readonly ICommandHandler<UploadApplicationTranslationsCommand> _uploadApplicationTranslationsCommandHandler;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;

        private readonly IRequestContextProvider _requestContextProvider;

        private IMapper _mapper;

        public ApplicationTranslationService(
            IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>> applicationTranslationsQueryHandler,
            ICommandHandler<UpdateApplicationTranslationCommand> updateApplicationTranslationsCommandHandler,
            ICommandHandler<DownloadApplicationTranslationsCommand> downloadApplicationTranslationsCommandHandler,
            ICommandHandler<UploadApplicationTranslationsCommand> uploadApplicationTranslationsCommandHandler,
            IApplicationTranslationRepository applicationTranslationRepository,
            IRequestContextProvider requestContextProvider,
            IMapper mapper
        )
        {
            _applicationTranslationsQueryHandler = applicationTranslationsQueryHandler;
            _updateApplicationTranslationsCommandHandler = updateApplicationTranslationsCommandHandler;
            _downloadApplicationTranslationsCommandHandler = downloadApplicationTranslationsCommandHandler;
            _uploadApplicationTranslationsCommandHandler = uploadApplicationTranslationsCommandHandler;
            _applicationTranslationRepository = applicationTranslationRepository;
            _requestContextProvider = requestContextProvider;
            _mapper = mapper;
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
                })
            };

            await _updateApplicationTranslationsCommandHandler.HandleAsync(command);
            var result = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId,
                Codes = new List<string>() { code }
            });

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(result);
        }

        public async Task<IEnumerable<ApplicationTranslationViewModel>> GetMany(ApplicationTranslationViewModel filter)
        {
            var query = new ApplicationTranslationsQuery()
            {
                LanguageCode = filter.LanguageCode,
                TranslationCode = filter.TranslationCode
            };

            var results = await _applicationTranslationsQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(results);
        }

        public async Task Download(Stream file)
        {
            var context = _requestContextProvider.Context;
            var command = new DownloadApplicationTranslationsCommand()
            {
                ApplicationId = context.ApplicationId,
                File = file
            };

            await _downloadApplicationTranslationsCommandHandler.HandleAsync(command);
        }

        public async Task<IEnumerable<ApplicationTranslationViewModel>> Upload(IEnumerable<ApplicationTranslationsColumnViewModel> languagesCodes, Stream file)
        {

            var context = _requestContextProvider.Context;

            var command = new UploadApplicationTranslationsCommand()
            {
                LanguagesCodes = languagesCodes.Select(lc => new ApplicationTranslationColumnIndex()
                {
                    Index = lc.Index,
                    LanguageCode = lc.LanguageCode
                }),
                ApplicationId = context.ApplicationId,
                File = file
            };

            await _uploadApplicationTranslationsCommandHandler.HandleAsync(command);
            var result = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = context.ApplicationId
            });

            return _mapper.Map<IEnumerable<ApplicationTranslation>, IEnumerable<ApplicationTranslationViewModel>>(result);
        }
    }
}