using System;

namespace Foundation.Template.Admin.ViewModels
{
    public class UpdateEntityPropertyTranslationViewModel
    {
        // [FromRoute]
        // public Guid EntityPropertyId { get; set; }

        public string LanguageCode { get; set; }
        public string Label { get; set; }
        public string CategoryLabel { get; set; }
    }
}