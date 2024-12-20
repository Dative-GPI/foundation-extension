using System;
using System.Collections.Generic;

using Bones.Flow;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Core
{
    public class UpdateRoleOrganisationPermissionOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => new[] { "app.roleorganisation.update" };

        public Guid RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}