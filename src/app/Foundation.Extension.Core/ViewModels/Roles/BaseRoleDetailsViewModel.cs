using System;
using System.Collections.Generic;

namespace Foundation.Extension.Core.ViewModels
{
    public class BaseRoleDetailsViewModel
    {
        public Guid Id { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}