using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IColumnRepository
    {
        Task<IEnumerable<Column>> GetMany(ColumnsFilter filter);
        Task<IEntity<Guid>> Create(CreateColumn payload);
        Task CreateRange(IEnumerable<CreateColumn> payload);
        Task<IEntity<Guid>> Update(UpdateColumn payload);
        Task UpdateRange(IEnumerable<UpdateColumn> payload);
    }
}