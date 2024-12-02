using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class EntityPropertiesQuery : ICoreRequest, IRequest<IEnumerable<EntityProperty>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_ENTITYPROPERTY_INFOS };

        public string Prefix { get; set; }
    }
}