using System.Linq;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin
{
    public class PermissionApplicationsQuery : ICoreRequest, IRequest<IEnumerable<PermissionApplicationInfos>>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();

        public string Search { get; set; }
    }
}