using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.CoreAuthorizations;

namespace Foundation.Extension.Core
{
    public class UserOrganisationQuery : ICoreRequest, IRequest<UserOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => new List<string>() { APP_USERORGANISATION_READ };

        public Guid UserOrganisationId { get; set; }
    }
}