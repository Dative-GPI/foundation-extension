using System;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreateEntityPropertyApplicationTranslation
    {
        public required Guid ApplicationId { get; set; }
        public required Guid EntityPropertyId { get; set; }
        public required string Label { get; set; }
        public required string LanguageCode { get; set; }
    }
}