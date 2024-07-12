using System;
using System.Threading.Tasks;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Domain.Repositories.Interfaces 
{
    public interface IApplicationRepository 
    {
        Task<ApplicationDetails> Get(Guid applicationId);
        Task<IEntity<Guid>> Create(CreateApplication payload);
        Task<IEntity<Guid>> Update(UpdateApplication payload);
        Task Remove(Guid id);
    }
}