using System;
using System.Collections.Generic;

using Bones.Flow;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin.Requests
{
    public class ApplicationTranslationsSpreadsheetQuery : ICoreRequest, IRequest<byte[]>
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_APPLICATIONTRANSLATION_INFOS };
        public Guid ApplicationId { get; set; }
    }
}