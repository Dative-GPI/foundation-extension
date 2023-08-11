using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Template.Domain.Repositories.Interfaces;

namespace Foundation.Template.Gateway.Handlers
{
    public class ThumbnailImageQueryHandler : IMiddleware<ThumbnailImageQuery, byte[]>
    {
        private IImageRepository _imageRepository;

        public ThumbnailImageQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        
        public async Task<byte[]> HandleAsync(ThumbnailImageQuery request, Func<Task<byte[]>> next, CancellationToken cancellationToken)
        {
            return await _imageRepository.GetThumbnail(request.Id);
        }
    }
}