using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class PermissionApplicationDetails : ITranslatable<TranslationPermissionApplication>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        #region Translated properties
        public string Label { get; set; }
        #endregion
        
        public List<TranslationPermissionApplication> Translations { get; set; }
    }
}