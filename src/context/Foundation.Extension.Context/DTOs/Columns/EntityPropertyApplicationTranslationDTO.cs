using System;
using System.Collections.Generic;

using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
    // Donne la liste des propriétés possibles pour un type d'entité afin de construire
    // les tables (colonnes) côté front 
    public class EntityPropertyApplicationTranslationDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid EntityPropertyId { get; set; }
        public EntityPropertyDTO EntityProperty { get; set; }

        public Guid ApplicationId { get; set; }
        public ApplicationDTO Application { get; set; }

        public string Label { get; set; }
        public string CategoryLabel { get; set; }

        public string LanguageCode { get; set; }
        // public LanguageDTO Language { get; set; }

        public bool Disabled { get; set; }
    }
}