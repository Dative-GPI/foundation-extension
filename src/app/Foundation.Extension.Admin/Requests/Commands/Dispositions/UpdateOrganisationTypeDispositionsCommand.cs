using System;
using System.Collections.Generic;

using Bones.Flow;
using Bones.Repository.Interfaces;

using static Foundation.Extension.Admin.Authorizations;

namespace Foundation.Extension.Admin
{
    public class UpdateOrganisationTypeTableCommand : ICoreRequest, IRequest<IEnumerable<IEntity<Guid>>>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_COLUMNORGANISATIONTYPE_UPDATE };
        public Guid ApplicationId { get; set; }
        public Guid ActorId { get; set; }

        public Guid OrganisationTypeId { get; set; }
        public Guid TableId { get; set; }
        public IEnumerable<UpdateOrganisationTypeDisposition> OrganisationTypeDispositions { get; set; }
    }

    public class UpdateOrganisationTypeDisposition
    {
        public Guid ColumnId { get; set; }
        public bool Disabled { get; set; }
        public bool Hidden { get; set; }
        public int Index { get; set; }
    }
}