using System;
using System.Collections.Generic;

using Bones.Repository.Interfaces;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Context.DTOs
{
    public class PermissionApplicationDTO : IEntity<Guid>, ICodeEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LabelDefault { get; set; }
        public List<TranslationPermissionApplicationDTO> Translations { get; set; }
        public bool Disabled { get; set; }
    }

    public class TranslationPermissionApplicationDTO
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}
