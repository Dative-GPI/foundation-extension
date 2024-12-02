using System;
using System.Collections.Generic;
using System.Text.Json;

using Bones.Flow;
using Bones.Repository.Interfaces;

using static Foundation.Extension.Admin.Authorizations;

namespace Foundation.Extension.Admin
{
    public class UpdateRolePermissionOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => new[] { "admin.organisation-type.role.update" };

        public Guid RoleOrganisationId { get; set; }
        public List<Guid> PermissionIds { get; set; }
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
    }
}