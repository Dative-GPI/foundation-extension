using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;


namespace Foundation.Extension.Admin
{
    public class RolePermissionOrganisationQuery : ICoreRequest, IRequest<RolePermissionOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_ORGANISATION_TYPE_ROLE_INFOS };

        public Guid RoleOrganisationId { get; set; }
    }
}