using System;
using System.Collections.Generic;
using System.Linq;

using Bones.Flow;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Core
{
    public class UpdateRolePermissionOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();

        public Guid RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}