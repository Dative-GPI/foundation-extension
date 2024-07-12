using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<byte[]> GetRaw(Guid imageId);
        Task<byte[]> GetThumbnail(Guid imageId);
        Task<IEntity<Guid>> Create(CreateImage payload);
    }
}