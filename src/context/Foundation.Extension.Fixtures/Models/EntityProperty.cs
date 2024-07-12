using System;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Fixtures
{
    public class EntityProperty : ICodeEntity
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string EntityType { get; set; }
        public string LabelDefault { get; set; }

        public string ParentId { get; set; }
    }
}