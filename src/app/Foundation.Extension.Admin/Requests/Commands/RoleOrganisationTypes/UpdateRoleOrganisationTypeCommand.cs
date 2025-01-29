using System;
using System.Collections.Generic;
using System.Text.Json;

using Bones.Flow;
using Bones.Repository.Interfaces;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class UpdateRoleOrganisationTypeCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => new List<string>() { ADMIN_ORGANISATION_TYPE_ROLE_UPDATE };

        public Guid RoleOrganisationTypeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
    }
}