using System;
using System.Collections.Generic;
using System.IO;
using Bones.Flow;

using static Foundation.Clients.AdminAuthorizations;


namespace Foundation.Extension.Admin.Requests
{
    public class DownloadApplicationTranslationsCommand : ICoreRequest, IRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_APPLICATIONTRANSLATION_INFOS };

        public required Guid ApplicationId { get; set; }

        public required Stream File { get; set; }
    }
}