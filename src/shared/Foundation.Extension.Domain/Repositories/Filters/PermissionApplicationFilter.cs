using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class PermissionApplicationFilter
    {
        public string Search { get; set; }
        public IEnumerable<Guid> PermissionApplicationIds { get; set; }
    }
}