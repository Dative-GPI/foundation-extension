using System;
using System.Threading.Tasks;

using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Abstractions
{
    public interface IImageService
    {
        Task<ImageDetailsViewModel> Get(Guid id);
        Task<byte[]> GetRaw(Guid id);
        Task<byte[]> GetThumbnail(Guid id);
    }
}