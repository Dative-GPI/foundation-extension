using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IEntityPropertyApplicationTranslationRepository
    {
        Task<IEnumerable<EntityPropertyApplicationTranslation>> GetMany(EntityPropertyApplicationTranslationsFilter filter);
        Task CreateRange(IEnumerable<CreateEntityPropertyApplicationTranslation> payload);
        Task RemoveRange(IEnumerable<Guid> ids);
    }
}