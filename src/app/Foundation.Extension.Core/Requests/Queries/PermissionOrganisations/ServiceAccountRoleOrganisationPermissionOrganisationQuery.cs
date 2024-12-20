using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class ServiceAccountRoleOrganisationPermissionOrganisationQuery : ICoreRequest, IRequest<RolePermissionOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => new string[] { "app.serviceaccountorganisation.manage" };

        public Guid RoleId { get; set; }
    }
}