using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.CoreAuthorizations;

namespace Foundation.Extension.Core
{
    public class ServiceAccountRoleOrganisationQuery : ICoreRequest, IRequest<ServiceAccountRoleOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => new List<string>() { APP_SERVICEACCOUNTORGANISATION_MANAGE };

        public Guid ServiceAccountRoleOrganisationId { get; set; }
    }
}