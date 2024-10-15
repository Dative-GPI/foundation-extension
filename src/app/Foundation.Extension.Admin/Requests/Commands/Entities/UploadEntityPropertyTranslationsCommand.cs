using System;
using System.IO;
using System.Collections.Generic;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin
{
    public class UploadEntityPropertyTranslationsCommand : ICoreRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_APPLICATIONTRANSLATION_UPDATE };
        public Guid ApplicationId { get; set; }

        public required List<SpreadsheetColumnDefinition> Languages { get; set; }
        public required Stream File { get; set; }
    }
}