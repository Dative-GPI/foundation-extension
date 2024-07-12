using System;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreateEntityProperty
    {
        public required string Code { get; set; }
        public required string LabelDefault { get; set; }
        public required string Value { get; set; }
        public required string EntityType { get; set; }
        public required bool Disabled { get; set; }
    }
}