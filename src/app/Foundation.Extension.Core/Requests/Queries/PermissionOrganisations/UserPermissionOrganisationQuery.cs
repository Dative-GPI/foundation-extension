using System;
using System.Collections.Generic;
using System.Linq;

using Bones.Flow;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class UserPermissionOrganisationQuery : ICoreRequest, IRequest<UserPermissionOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();

        public Guid UserId { get; set; }
        public UserType UserType { get; set; }
    }
}