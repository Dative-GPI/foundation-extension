using System;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
    public class UpdateColumnViewModel
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Index { get; set; }
        public bool Hidden { get; set; }
        public bool Sortable { get; set; }
        public bool Filterable { get; set; }
        public bool Configurable { get; set; }
        public bool Disabled { get; set; }
    }
}