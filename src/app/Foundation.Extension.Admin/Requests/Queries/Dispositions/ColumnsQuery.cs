using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Extension.Admin.Authorizations;

namespace Foundation.Extension.Admin
{
    public class ColumnsQuery : ICoreRequest, IRequest<IEnumerable<Column>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_COLUMN_INFOS };
		public Guid ApplicationId { get; set; }
        public Guid ActorId { get; set; }

        public string TableCode { get; set; }
    }
}