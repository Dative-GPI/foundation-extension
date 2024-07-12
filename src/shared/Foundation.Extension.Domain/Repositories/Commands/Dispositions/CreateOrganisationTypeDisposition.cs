using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreateOrganisationTypeDisposition
    {
        public required Guid OrganisationTypeId { get; set; }
        public required Guid ColumnId { get; set; }
        public required int Index { get; set; }
        public required bool Hidden { get; set; }
        public required bool Disabled { get; set; }
    }
}