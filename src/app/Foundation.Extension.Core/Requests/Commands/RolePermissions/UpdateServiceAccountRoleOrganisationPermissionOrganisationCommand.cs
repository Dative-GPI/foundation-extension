using System;
using System.Collections.Generic;

using Bones.Flow;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Core
{
    public class UpdateServiceAccountRoleOrganisationPermissionOrganisationCommand : ICoreRequest, IRequest<IEntity<Guid>>
    {
        public IEnumerable<string> Authorizations => new[] { "app.serviceaccountorganisation.manage" };

        public Guid RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}