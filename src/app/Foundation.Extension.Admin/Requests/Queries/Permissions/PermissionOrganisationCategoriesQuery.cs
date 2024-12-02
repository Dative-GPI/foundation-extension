using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class PermissionOrganisationCategoriesQuery : ICoreRequest, IRequest<IEnumerable<PermissionOrganisationCategory>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_PERMISSION_CATEGORY };
    }
}