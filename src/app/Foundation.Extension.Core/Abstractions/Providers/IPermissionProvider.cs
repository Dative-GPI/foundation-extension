using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IPermissionProvider
    {
        Task<IEnumerable<string>> GetPermissions();
        Task<bool> HasPermissions(params string[] permissions);
    }
}