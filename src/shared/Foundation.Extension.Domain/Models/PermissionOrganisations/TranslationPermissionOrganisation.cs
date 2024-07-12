using System;

namespace Foundation.Extension.Domain.Models
{
    public class TranslationPermissionOrganisation : ITranslation
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}