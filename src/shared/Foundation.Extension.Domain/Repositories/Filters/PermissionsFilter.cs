using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class PermissionsFilter
    {
        public string Search { get; set; }
        public IEnumerable<Guid> PermissionIds { get; set; }
    }
}