using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class ApplicationTranslationFilter
    {
        public Guid ApplicationId { get; set; }
        public string LanguageCode { get; set; }
        public IEnumerable<string> Codes { get; set; }
    }
}