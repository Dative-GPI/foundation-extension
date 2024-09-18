using System;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Fixtures
{
    public class Table : ICodeEntity
    {
        public string Code { get; set; }
        public string LabelDefault { get; set; }
        public string EntityType { get; set; }
    }
}