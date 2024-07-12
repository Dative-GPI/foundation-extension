using System;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class UpdateEntityProperty
    {
        public required Guid EntityPropertyId { get; set; }
        public required string LabelDefault { get; set; }
    }
}