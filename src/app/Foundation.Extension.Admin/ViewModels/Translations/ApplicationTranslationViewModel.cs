using System;

namespace Foundation.Extension.Admin.ViewModels
{
    public class ApplicationTranslationViewModel
    {
        public Guid Id { get; set; }
        public string Code => TranslationCode;
        public string TranslationCode { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
    }
}