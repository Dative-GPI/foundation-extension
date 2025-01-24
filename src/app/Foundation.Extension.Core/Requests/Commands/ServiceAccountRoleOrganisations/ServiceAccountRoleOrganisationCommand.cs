using System;
using System.Collections.Generic;
using System.Text.Json;

using Bones.Flow;
using Bones.Repository.Interfaces;

using static Foundation.Clients.CoreAuthorizations;

namespace Foundation.Extension.Core
{
    public class UpdateServiceAccountRoleOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => new List<string>() { APP_SERVICEACCOUNTORGANISATION_MANAGE };

        public Guid ServiceAccountRoleOrganisationId { get; set; }
        public List<Guid> PermissionIds { get; set; }
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
    }
}