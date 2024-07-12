using System;
using System.Collections.Generic;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Clients;

namespace Foundation.Extension.Core
{
    public class UpdateRolePermissionOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => new[] { "app.role.update" };

        public Guid RoleOrganisationId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}