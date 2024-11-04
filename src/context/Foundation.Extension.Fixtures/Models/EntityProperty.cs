using System;
using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Fixtures
{
    public class EntityProperty : ICodeEntity
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string EntityType { get; set; }
        public string LabelDefault { get; set; }
		public string TranslationCode { get; set; }		
		public EntityKind EntityKind { get; set; }
		public string Context { get; set; }

        public string ParentId { get; set; }
    }
}