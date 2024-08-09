using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Gateway.Handlers
{
  public class ImageQueryHandler : IMiddleware<ImageQuery, ImageDetails>
  {
    private readonly IImageRepository _imageRepository;

    public ImageQueryHandler(IImageRepository imageRepository)
    {
      _imageRepository = imageRepository;
    }

    public async Task<ImageDetails> HandleAsync(ImageQuery request, Func<Task<ImageDetails>> next, CancellationToken cancellationToken)
    {
      return await _imageRepository.Get(request.Id);
    }
  }
}