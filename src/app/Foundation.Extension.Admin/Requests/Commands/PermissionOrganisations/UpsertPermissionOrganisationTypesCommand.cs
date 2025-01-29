using System;
using System.Collections.Generic;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class UpsertPermissionOrganisationTypesCommand : ICoreRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_PERMISSIONORGANISATIONTYPE_UPDATE };
        public List<UpsertPermissionOrganisationTypes> PermissionOrganisationTypes { get; set; }
    }

    public class UpsertPermissionOrganisationTypes
    {
        public Guid OrganisationTypeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}