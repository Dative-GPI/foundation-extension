using System;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.Models;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Services
{
    public class FileService : IFileService
    {
        private readonly IQueryHandler<FileQuery, FileDetails> _fileQueryHandler;
        private readonly IMapper _mapper;

        public FileService
        (
            IQueryHandler<FileQuery, FileDetails> fileQueryHandler,
            IMapper mapper
        )
        {
            _fileQueryHandler = fileQueryHandler;
            _mapper = mapper;
        }

        public async Task<FileDetailsViewModel> Get(Guid id)
        {
            var query = new FileQuery()
            {
                FileId = id
            };

            var result = await _fileQueryHandler.HandleAsync(query);

            return _mapper.Map<FileDetails, FileDetailsViewModel>(result);
        }
    }
}