using System;
using System.Collections.Generic;
using Bones.Repository.Interfaces;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Context.DTOs
{
    public class PermissionApplicationCategoryDTO : IEntity<Guid>, ICodeEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LabelDefault { get; set; }
        public string Prefix { get; set; }
        public List<TranslationPermissionApplicationCategoryDTO> Translations { get; set; }
        public bool Disabled { get; set; }
    }

    public class TranslationPermissionApplicationCategoryDTO
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}