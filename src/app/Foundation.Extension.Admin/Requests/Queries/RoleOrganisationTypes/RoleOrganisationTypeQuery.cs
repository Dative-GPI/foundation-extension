using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;


namespace Foundation.Extension.Admin
{
    public class RoleOrganisationTypeQuery : ICoreRequest, IRequest<RoleOrganisationTypeDetails>
    {
        public IEnumerable<string> Authorizations => new List<string>() { ADMIN_ORGANISATION_TYPE_ROLE_INFOS };

        public Guid RoleOrganisationTypeId { get; set; }
    }
}