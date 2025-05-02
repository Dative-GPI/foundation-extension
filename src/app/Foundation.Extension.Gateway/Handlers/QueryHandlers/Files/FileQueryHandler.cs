using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Bones.Flow;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Gateway.Handlers
{
    public class FileQueryHandler : IMiddleware<FileQuery, FileDetails>
    {
        private readonly IFileRepository _fileRepository;

        public FileQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileDetails> HandleAsync(FileQuery request, Func<Task<FileDetails>> next, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.Get(request.FileId);

            if (file == null)
            {
                throw new Exception(ErrorCode.EntityNotFound);
            }

            return file;
        }
    }
}