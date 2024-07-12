using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface ITranslationRepository
    {
        Task<IEnumerable<Translation>> GetMany();
    }
}