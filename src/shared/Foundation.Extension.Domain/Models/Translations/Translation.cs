using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class Translation
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }

        public List<TranslationTranslation> Translations { get; set; }
    }

    public class TranslationTranslation : ITranslation
    {
        public string LanguageCode { get; set; }
        public string Value { get; set; }
    }
}