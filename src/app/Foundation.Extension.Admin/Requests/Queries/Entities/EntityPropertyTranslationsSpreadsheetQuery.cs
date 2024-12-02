using System;
using System.Collections.Generic;

using Bones.Flow;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class EntityPropertyTranslationsSpreadsheetQuery : ICoreRequest, IRequest<byte[]>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_ENTITYPROPERTY_INFOS };
        public Guid ApplicationId { get; set; }
    }
}