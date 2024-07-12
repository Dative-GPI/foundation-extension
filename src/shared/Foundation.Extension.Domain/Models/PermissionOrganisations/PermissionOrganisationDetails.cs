using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class PermissionOrganisationDetails : ITranslatable<TranslationPermissionOrganisation>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        #region Translated properties
        public string Label { get; set; }
        #endregion
        
        public List<TranslationPermissionOrganisation> Translations { get; set; }
    }
}