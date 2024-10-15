using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class EntityPropertyApplicationTranslationsFilter
    {
        public Guid? ApplicationId { get; set; }
        public Guid? EntityPropertyId { get; set; }
        public List<Guid> EntityPropertiesIds { get; set; }
        public string EntityType { get; set; }
        public string LanguageCode { get; set; }
    }
}