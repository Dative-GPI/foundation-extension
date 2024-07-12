using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class ApplicationTranslationsFilter
    {
        public Guid ApplicationId { get; set; }
        public string LanguageCode { get; set; }
        public string TranslationCode { get; set; }
        public IEnumerable<string> Codes { get; set; }
    }
}