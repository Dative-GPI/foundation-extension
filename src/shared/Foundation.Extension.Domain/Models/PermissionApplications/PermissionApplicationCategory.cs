using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class PermissionApplicationCategory: ITranslatable<TranslationPermissionApplicationCategory>
    {
        public string Label { get; set; }
        public string Prefix { get; set; }
        public List<TranslationPermissionApplicationCategory> Translations { get; set; }
    }

    public class TranslationPermissionApplicationCategory: ITranslation
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}