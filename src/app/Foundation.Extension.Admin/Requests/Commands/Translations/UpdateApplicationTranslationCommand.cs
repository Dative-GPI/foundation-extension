using System.Collections.Generic;

using Bones.Flow;

using static Foundation.Clients.AdminAuthorizations;

namespace Foundation.Extension.Admin.Requests
{
    public class UpdateApplicationTranslationCommand : ICoreRequest, IRequest
    {
        public IEnumerable<string> Authorizations => new[] { ADMIN_APPLICATIONTRANSLATION_UPDATE };

        public string Code { get; set; }
        public IEnumerable<UpdateApplicationTranslationLanguageCommand> Translations { get; set; }
    }

    public class UpdateApplicationTranslationLanguageCommand
    {
        public string LanguageCode { get; set; }
        public string Value { get; set; }
    }
}