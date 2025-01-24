using System;
using System.Collections.Generic;
using System.Linq;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin
{
    public class RoleApplicationQuery : ICoreRequest, IRequest<RoleApplicationDetails>
    {
        public IEnumerable<string> Authorizations => Enumerable.Empty<string>();
        
        public Guid RoleApplicationId { get; set; }
    }
}