using System;

namespace Foundation.Extension.Domain.Models
{
    public class ApplicationTranslation
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string TranslationCode { get; set; }
        public string Value { get; set; }
    }
}