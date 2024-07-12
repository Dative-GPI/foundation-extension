using System;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
    public class UpdateTableViewModel
    {
        public IEnumerable<UpdateColumnViewModel> Columns { get; set; }
    }
}