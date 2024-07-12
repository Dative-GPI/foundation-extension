using System;
using System.Collections.Generic;
using System.IO;
using Bones.Flow;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin.Requests
{
    public class UploadApplicationTranslationsCommand : ICoreRequest, IRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_APPLICATIONTRANSLATION_UPDATE };

        public required IEnumerable<ApplicationTranslationColumnIndex> LanguagesCodes { get; set; }
        public required Guid ApplicationId { get; set; }
        public required Stream File { get; set; }
    }

    public class ApplicationTranslationColumnIndex
    {
        public int Index { get; set; }
        public string LanguageCode { get; set; }
    }
}