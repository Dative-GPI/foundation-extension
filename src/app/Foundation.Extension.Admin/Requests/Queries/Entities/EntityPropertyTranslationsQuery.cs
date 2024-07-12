using System;
using System.Collections.Generic;
using System.Linq;
using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class EntityPropertyTranslationsQuery : ICoreRequest, IRequest<IEnumerable<EntityPropertyTranslation>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_ENTITYPROPERTYTRANSLATIONS_INFOS };
        public Guid ApplicationId { get; set; }

        public string Prefix { get; set; }
        public Guid? EntityPropertyId { get; set; }
        public List<Guid> EntityPropertyIds { get; set; }
    }
}