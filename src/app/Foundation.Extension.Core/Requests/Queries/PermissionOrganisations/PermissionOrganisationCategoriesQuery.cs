using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Clients;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class PermissionOrganisationCategoriesQuery : ICoreRequest, IRequest<IEnumerable<PermissionOrganisationCategory>>
    {
        public IEnumerable<string> Authorizations => new string[] {};
    }
}