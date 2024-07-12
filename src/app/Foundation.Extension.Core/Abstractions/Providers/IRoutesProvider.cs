using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRoutesProvider
    {
        Task<IEnumerable<RouteInfos>> GetRoutes();
    }
}