using System;

namespace Foundation.Template.Domain.Repositories.Filters
{
    public class EntityPropertiesFilter
    {
        public Guid ApplicationId { get; set; }
        public string EntityType { get; set; }
    }
}