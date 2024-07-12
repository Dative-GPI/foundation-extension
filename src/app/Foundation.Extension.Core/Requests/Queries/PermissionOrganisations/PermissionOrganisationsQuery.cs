using System;
using System.Linq;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class PermissionOrganisationsQuery : ICoreRequest, IRequest<IEnumerable<PermissionOrganisationInfos>>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();
    }
}