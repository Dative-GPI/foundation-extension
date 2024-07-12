using System;
using System.Linq;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin
{
    public class PermissionApplicationCategoriesQuery : ICoreRequest, IRequest<IEnumerable<PermissionApplicationCategory>>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();
    }
}