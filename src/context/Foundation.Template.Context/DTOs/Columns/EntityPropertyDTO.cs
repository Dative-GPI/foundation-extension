using System;
using System.Collections.Generic;

using Bones.Repository.Interfaces;
using Foundation.Template.Context.Abstractions;

namespace Foundation.Template.Context.DTOs
{
    // Donne la liste des propriétés possibles pour un type d'entité afin de construire
    // les tables (colonnes) côté front 
    public class EntityPropertyDTO : IEntity<Guid>, ICodeEntity
    {
        public Guid Id { get; set; }

        public string LabelDefault { get; set; }
        public string CategoryLabelDefault { get; set; }
        
        // PropertyName
        public string Code { get; set; }
        public string Value { get; set; }
        
        // Class FullName
        public string EntityType { get; set; }
        public bool Disabled { get; set; }
    }

    public class TranslationEntityPropertyDTO
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
        public string CategoryLabel { get; set; }
    }
}