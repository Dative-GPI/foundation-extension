using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class EntityPropertyTranslationsFilter
    {
        public Guid? ApplicationId { get; set; }
        public Guid? EntityPropertyId { get; set; }
        public List<Guid> EntityPropertyIds { get; set; }
        public string EntityType { get; set; }
    }
}