using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class EntityPropertyTranslationsQuery : ICoreRequest, IRequest<IEnumerable<EntityPropertyApplicationTranslation>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_ENTITYPROPERTYAPPLICATIONTRANSLATIONS_INFOS };
        
        public Guid? EntityPropertyId { get; set; }
        public List<Guid> EntityPropertiesIds { get; set; }
    }
}