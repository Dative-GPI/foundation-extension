using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

using static Foundation.Clients.AdminAuthorizations;


namespace Foundation.Extension.Admin.Requests
{
    public class TranslationsQuery : ICoreRequest, IRequest<IEnumerable<Translation>>
    {
        public IEnumerable<string> Authorizations => new string[] { ADMIN_TRANSLATION_INFOS };
    }
}