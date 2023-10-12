using System;
using System.Collections.Generic;

namespace Foundation.Template.Domain.Models
{
    public class PermissionOrganisationInfos : ITranslatable<TranslationPermissionOrganisation>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        #region Translated properties
        public string Label { get; set; }
        #endregion
        
        public List<TranslationPermissionOrganisation> Translations { get; set; }
    }
}