using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreatePermissionOrganisationType
    {
        public Guid OrganisationTypeId { get; set; }
        public Guid PermissionId { get; set; }
    }
}