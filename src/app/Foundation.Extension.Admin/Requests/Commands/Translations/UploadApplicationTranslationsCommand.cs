using System;
using System.Collections.Generic;
using System.IO;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin.Requests
{
    public class UploadApplicationTranslationsCommand : ICoreRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_APPLICATIONTRANSLATION_UPDATE };
        public Guid ApplicationId { get; set; }

        public required IEnumerable<SpreadsheetColumnDefinition> Languages { get; set; }
        public required Stream File { get; set; }
    }
}