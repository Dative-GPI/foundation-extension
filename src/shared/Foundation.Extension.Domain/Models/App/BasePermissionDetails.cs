using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class BasePermissionDetails
    {
        public Guid Id { get; set; }
        public List<PermissionItem> Permissions { get; set; }
    }
}