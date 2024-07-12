using System;
using System.Collections.Generic;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class UpdateColumn
    {
        public required Guid Id { get; set; }
        public required bool Sortable { get; set; }
        public required bool Filterable { get; set; }
        public required bool Configurable { get; set; }
        public required bool Hidden { get; set; }
        public required int Index { get; set; }
        public required bool Disabled { get; set; }
    }
}