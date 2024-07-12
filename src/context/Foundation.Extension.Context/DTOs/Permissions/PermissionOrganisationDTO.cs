using System;
using System.Collections.Generic;

using Bones.Repository.Interfaces;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Context.DTOs
{
    public class PermissionOrganisationDTO : IEntity<Guid>, ICodeEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LabelDefault { get; set; }
        public List<TranslationPermissionOrganisationDTO> Translations { get; set; }
        public bool Disabled { get; set; }
    }

    public class TranslationPermissionOrganisationDTO
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
    }
}