using System;
using System.Threading.Tasks;
using AutoMapper;
using Bones.Flow;

using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.Models;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Services
{
  public class ImageService : IImageService
  {
    private readonly IMapper _mapper;
    private readonly IQueryHandler<ImageQuery, ImageDetails> _imageQueryHandler;
    private IQueryHandler<RawImageQuery, byte[]> _rawImageQueryHandler;
    private IQueryHandler<ThumbnailImageQuery, byte[]> _thumbnailImageQueryHandler;

    public ImageService(
    IMapper mapper,
    IQueryHandler<ImageQuery, ImageDetails> imageQueryHandler,
        IQueryHandler<RawImageQuery, byte[]> rawImageQueryHandler,
        IQueryHandler<ThumbnailImageQuery, byte[]> thumbnailImageQueryHandler
    )
    {
      _mapper = mapper;
      _imageQueryHandler = imageQueryHandler;
      _rawImageQueryHandler = rawImageQueryHandler;
      _thumbnailImageQueryHandler = thumbnailImageQueryHandler;
    }

    public async Task<ImageDetailsViewModel> Get(Guid id)
    {
      var request = new ImageQuery()
      {
        Id = id
      };

      var result = await _imageQueryHandler.HandleAsync(request);

      return _mapper.Map<ImageDetails, ImageDetailsViewModel>(result);
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