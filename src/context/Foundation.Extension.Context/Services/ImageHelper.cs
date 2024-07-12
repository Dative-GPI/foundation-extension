using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Blurhash.ImageSharp;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Context.Configurations;

namespace Foundation.Extension.Context.Services
{
    public class ImageHelper : IImageHelper
    {
        private ILogger<ImageHelper> _logger;

        public ImageHelper(ILogger<ImageHelper> logger)
        {
            _logger = logger;
        }

        public async Task<Models.Image> Compute(byte[] data, int maxSize)
        {
            using var image = SixLabors.ImageSharp.Image.Load(data);

            using var dataStream = new MemoryStream();
            using var thumbnailStream = new MemoryStream();
            using var jpgStream = new MemoryStream();

            var thumbnail = image.Clone(
                x => x.Resize(new ResizeOptions()
                {
                    Size = new Size(90),
                    Mode = ResizeMode.Min
                })
            );

            var raw = image.Clone(
                x => x.Resize(new ResizeOptions()
                {
                    Size = new Size(maxSize),
                    Mode = ResizeMode.Min
                })
            );

            await raw.SaveAsJpegAsync(jpgStream);
            jpgStream.Position = 0;

            var jpg = SixLabors.ImageSharp.Image.Load<Rgb24>(jpgStream);

            var blurHash = Blurhasher.Encode(jpg, 4, 3);

            await raw.SaveAsPngAsync(dataStream);
            await thumbnail.SaveAsPngAsync(thumbnailStream);

            var png = dataStream.ToArray();
            var thumb = thumbnailStream.ToArray();

            var result = new Models.Image()
            {
                BlurHash = blurHash,
                Data = png,
                Thumbnail = thumb,
                Width = image.Bounds.Width,
                Height = image.Bounds.Height
            };

            return result;
        }

    }
}