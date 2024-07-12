using System;
using System.Collections.Generic;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Models
{
    public class Column
    {
        public Guid Id { get; set; }

        public Guid TableId { get; set; }
        public string TableCode { get; set; }

        public PropertyType PropertyType { get; set; }
        public Guid? EntityPropertyId { get; set; }
        public Guid? CustomPropertyId { get; set; }

        public string Code { get; set; }
        public string Value { get; set; }
        
        public string Label { get; set; }

        public bool Sortable { get; set; }
        public bool Filterable { get; set; }

        public int Index { get; set; }
        public bool Hidden { get; set; }
        public bool Disabled { get; set; }
    }
}