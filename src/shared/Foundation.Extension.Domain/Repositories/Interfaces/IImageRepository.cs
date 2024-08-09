using System;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<ImageDetails> Get(Guid imageId);
        Task<byte[]> GetRaw(Guid imageId);
        Task<byte[]> GetThumbnail(Guid imageId);
        Task<IEntity<Guid>> Create(CreateImage payload);
    }
}