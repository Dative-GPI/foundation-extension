using System;
using System.Collections.Generic;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Models
{
    public class EntityProperty
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string ParentId { get; set; }
        public string EntityType { get; set; }
		public string TranslationCode { get; set; }
		public EntityKind EntityKind { get; set; }
        public bool Disabled { get; set; }
	}
}