using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IPermissionProvider
    {
        Task<IEnumerable<string>> GetPermissions();
        Task<bool> HasPermissions(params string[] permissions);
    }
}