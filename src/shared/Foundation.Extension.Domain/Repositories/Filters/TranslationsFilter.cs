using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class TranslationsFilter
    {
        public IEnumerable<string> Codes { get; set; }
    }
}