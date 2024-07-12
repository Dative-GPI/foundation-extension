using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
    public class EntityPropertyTranslationsFilterViewModel
    {
        public Guid? EntityPropertyId { get; set; }

        public List<Guid> EntityPropertyIds { get; set; }
    }
}