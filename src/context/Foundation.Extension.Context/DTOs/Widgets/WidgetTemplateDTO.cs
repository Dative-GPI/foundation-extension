using System;
using System.Text.Json;
using System.Collections.Generic;
using Bones.Repository.Interfaces;

using Foundation.Clients.Core.FoundationModels;

namespace Foundation.Extension.Context.DTOs
{
    public class WidgetTemplateDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string LabelDefault { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string DescriptionDefault { get; set; }
        public WidgetCategory Category { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public JsonElement DefaultMeta { get; set; }
        public List<TranslationWidgetTemplateDTO> Translations { get; set; }
        public string Search { get; set; }
        public bool Disabled { get; set; }
        public NpgsqlTypes.NpgsqlTsVector SearchVector { get; set; }
    }

    public class TranslationWidgetTemplateDTO
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
    }
}