using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Gateway.Handlers
{
    public class RawImageQueryHandler : IMiddleware<RawImageQuery, byte[]>
    {
        private IImageRepository _imageRepository;

        public RawImageQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        
        public async Task<byte[]> HandleAsync(RawImageQuery request, Func<Task<byte[]>> next, CancellationToken cancellationToken)
        {
            return await _imageRepository.GetRaw(request.Id);
        }
    }
}