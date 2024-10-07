using System;
using System.Collections.Generic;
using Bones.Repository.Interfaces;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Context.DTOs
{
    public class TranslationDTO : IEntity<Guid>, ICodeEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ValueDefault { get; set; }
        public bool Disabled { get; set; }
        public List<TranslationTranslationDTO> Translations { get; set; }
    }

    public class TranslationTranslationDTO
    {
        public string LanguageCode { get; set; }
        public string Value { get; set; }
    }
}