using System;
using System.Collections.Generic;

using Bones.Flow;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Core
{
    public class UpdateRolePermissionOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => Array.Empty<string>();

        public Guid RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}