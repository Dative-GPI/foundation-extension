using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin
{
    public class RoleApplicationQuery : ICoreRequest, IRequest<RoleApplicationDetails>
    {
        public IEnumerable<string> Authorizations => new string[] {};
		public Guid ApplicationId { get; set; }
        public Guid ActorId { get; set; }
        
        public Guid RoleApplicationId { get; set; }
    }
}