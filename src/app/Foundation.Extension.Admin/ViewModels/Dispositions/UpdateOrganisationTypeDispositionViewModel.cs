using System;

namespace Foundation.Extension.Admin.ViewModels
{
    public class UpdateOrganisationTypeDispositionViewModel
    {
        public Guid ColumnId { get; set; }
        public int Index { get; set; }
        public bool Hidden { get; set; }
        public bool Disabled { get; set; }
    }
}