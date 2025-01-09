using System;
using System.Collections.Generic;
using System.Linq;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class RolePermissionOrganisationQuery : ICoreRequest, IRequest<RolePermissionOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();

        public Guid RoleId { get; set; }
    }
}