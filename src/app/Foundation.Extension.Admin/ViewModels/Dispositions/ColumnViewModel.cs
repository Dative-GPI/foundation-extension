using System;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
    public class ColumnViewModel
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public string TableCode { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyCode { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
        public bool Hidden { get; set; }
        public bool Sortable { get; set; }
        public bool Filterable { get; set; }
        public bool Configurable { get; set; }
        public bool Disabled { get; set; }
        public string Label { get; set; }
    }
}