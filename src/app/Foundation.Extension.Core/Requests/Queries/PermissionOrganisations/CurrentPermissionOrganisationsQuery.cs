using System.Collections.Generic;
using System.Linq;

using Bones.Flow;

namespace Foundation.Extension.Core
{
    public class CurrentPermissionOrganisationsQuery : ICoreRequest, IRequest<IEnumerable<string>>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();
    }
}