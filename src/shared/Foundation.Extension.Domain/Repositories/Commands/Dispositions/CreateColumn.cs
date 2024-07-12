using System;
using System.Collections.Generic;
using Foundation.Extension.Domain.Enums;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreateColumn
    {
        public required Guid ApplicationId { get; set; }
        public required Guid TableId { get; set; }

        public required PropertyType PropertyType { get; set; }
        public required Guid? EntityPropertyId { get; set; }
        public required Guid? CustomPropertyId { get; set; }

        public required bool Sortable { get; set; }
        public required bool Filterable { get; set; }

        public required int Index { get; set; }
        public required bool Hidden { get; set; }
        public required bool Disabled { get; set; }
    }
}