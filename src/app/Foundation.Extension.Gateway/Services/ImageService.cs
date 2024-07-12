using System;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Gateway.Abstractions;

namespace Foundation.Extension.Gateway.Services
{
    public class ImageService : IImageService
    {
        private IQueryHandler<RawImageQuery, byte[]> _rawImageQueryHandler;
        private IQueryHandler<ThumbnailImageQuery, byte[]> _thumbnailImageQueryHandler;

        public ImageService(
            IQueryHandler<RawImageQuery, byte[]> rawImageQueryHandler,
            IQueryHandler<ThumbnailImageQuery, byte[]> thumbnailImageQueryHandler
        )
        {
            _rawImageQueryHandler = rawImageQueryHandler;
            _thumbnailImageQueryHandler = thumbnailImageQueryHandler;
        }

        public async Task<byte[]> GetRaw(Guid id)
        {
            var request = new RawImageQuery()
            {
                Id = id
            };

            var result = await _rawImageQueryHandler.HandleAsync(request);

            return result;
        }

        public async Task<byte[]> GetThumbnail(Guid id)
        {
            var request = new ThumbnailImageQuery()
            {
                Id = id
            };

            var result = await _thumbnailImageQueryHandler.HandleAsync(request);

            return result;
        }
    }
}