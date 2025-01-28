using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class PermissionOrganisationCategoryInfos: ITranslatable<TranslationPermissionOrganisationCategory>
    {
        public string Label { get; set; }
        public string Prefix { get; set; }
        public List<TranslationPermissionOrganisationCategory> Translations { get; set; }
    }

    public class TranslationPermissionOrganisationCategory: ITranslation
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}