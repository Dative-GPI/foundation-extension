using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class UpdateRolePermissionOrganisation
    {
        public Guid Id { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}