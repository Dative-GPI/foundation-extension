using System;

namespace Foundation.Extension.Admin.ViewModels
{
    public class EntityPropertyTranslationViewModel
    {
        public Guid Id { get; set; }
        public Guid EntityPropertyId { get; set; }
        public string Label { get; set; }
        public string LanguageCode { get; set; }
    }
}