using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class EntityPropertiesFilter
    {
        public Guid ApplicationId { get; set; }
        public string EntityType { get; set; }
        public IEnumerable<Guid> EntityPropertyIds { get; set; }
    }
}