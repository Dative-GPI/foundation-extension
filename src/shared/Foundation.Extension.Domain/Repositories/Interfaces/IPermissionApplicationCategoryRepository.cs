using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface IPermissionApplicationCategoryRepository
    {
        Task<IEnumerable<PermissionApplicationCategoryInfos>> GetMany();
    }
}