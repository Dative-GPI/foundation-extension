using System;
using System.Collections.Generic;
using Bones.Flow;
using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Models;
using static Foundation.Extension.Admin.Authorizations;

namespace Foundation.Extension.Admin
{
    public class PatchTableCommand : ICoreRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_TABLE_PATCH };
        public Guid ApplicationId { get; set; }
        public Guid ActorId { get; set; }

        public Guid TableId { get; set; }
    }
}