using System;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<IEntity<Guid>> Create(CreateFile payload);
        Task<FileDetails> Get(Guid id);
        Task Remove(Guid id);
    }
}