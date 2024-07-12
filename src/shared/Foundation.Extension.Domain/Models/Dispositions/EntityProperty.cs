using System;

namespace Foundation.Extension.Domain.Models
{
    public class EntityProperty
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LabelDefault { get; set; }
        public string CategoryLabelDefault { get; set; }
        public string Value { get; set; }
        public string ParentId { get; set; }
        public string EntityType { get; set; }
        public bool Disabled { get; set; }
    }
}