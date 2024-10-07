using System;

namespace Foundation.Extension.Domain.Models
{
    public class EntityPropertyApplicationTranslation
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }
        public Guid EntityPropertyId { get; set; }
        public string EntityPropertyCode { get; set; }

        public string Label { get; set; }
        public string CategoryLabel { get; set; }
        public string LanguageCode { get; set; }
    }
}