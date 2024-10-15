using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class EntityProperty
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LabelDefault { get; set; }
        public string Value { get; set; }
        public string ParentId { get; set; }
        public string EntityType { get; set; }
        public bool Disabled { get; set; }
        public List<TranslationEntityProperty> Translations { get; set; }
    }

    public class TranslationEntityProperty
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}