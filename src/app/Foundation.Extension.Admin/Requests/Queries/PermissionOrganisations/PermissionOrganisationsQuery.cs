using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class PermissionOrganisationsQuery : ICoreRequest, IRequest<IEnumerable<PermissionOrganisationInfos>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_PERMISSION_INFOS };

        public string Search { get; set; }
    }
}