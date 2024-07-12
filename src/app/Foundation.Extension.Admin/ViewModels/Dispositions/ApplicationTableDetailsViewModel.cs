using System;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
    public class ApplicationTableDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public List<ColumnViewModel> Columns { get; set; }
    }
}