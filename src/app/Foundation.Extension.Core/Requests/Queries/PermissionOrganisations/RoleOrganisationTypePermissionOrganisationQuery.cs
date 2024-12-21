using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class RoleOrganisationTypePermissionOrganisationQuery : ICoreRequest, IRequest<RolePermissionOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => new string[] { "app.roleorganisationtype.read" };

        public Guid RoleId { get; set; }
    }
}