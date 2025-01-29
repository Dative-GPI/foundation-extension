using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Bones.Flow;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Admin
{
    public class UpdateRoleApplicationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();

        public Guid RoleApplicationId { get; set; }
        public List<Guid> PermissionIds { get; set; }
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
    }
}