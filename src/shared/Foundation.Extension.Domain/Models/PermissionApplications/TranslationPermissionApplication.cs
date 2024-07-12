using System;

namespace Foundation.Extension.Domain.Models
{
    public class TranslationPermissionApplication : ITranslation
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}