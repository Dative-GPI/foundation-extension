using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.CoreAuthorizations;

namespace Foundation.Extension.Core
{
    public class RoleOrganisationQuery : ICoreRequest, IRequest<RoleOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => new List<string>() { APP_ROLEORGANISATION_READ };

        public Guid RoleOrganisationId { get; set; }
    }
}