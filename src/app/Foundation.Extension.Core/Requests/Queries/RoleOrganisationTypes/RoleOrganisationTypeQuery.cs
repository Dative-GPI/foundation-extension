using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.CoreAuthorizations;

namespace Foundation.Extension.Core
{
    public class RoleOrganisationTypeQuery : ICoreRequest, IRequest<RoleOrganisationTypeDetails>
    {
        public IEnumerable<string> Authorizations => new List<string>() { APP_ROLEORGANISATIONTYPE_READ };

        public Guid RoleOrganisationTypeId { get; set; }
    }
}