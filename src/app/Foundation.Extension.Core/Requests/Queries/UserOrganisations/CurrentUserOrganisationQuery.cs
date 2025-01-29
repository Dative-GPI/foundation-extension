using System.Collections.Generic;
using System.Linq;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class CurrentUserOrganisationQuery : ICoreRequest, IRequest<UserOrganisationDetails>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();
    }
}