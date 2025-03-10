using System;
using System.Text.Json;
using System.Collections.Generic;

using Foundation.Clients.Core.FoundationModels;

namespace Foundation.Extension.Domain.Models
{
    public class WidgetTemplateDetails : ITranslatable<TranslationWidgetTemplate>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public WidgetCategory Category { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public JsonElement DefaultMeta { get; set; }

        #region Translated properties
        public string Label { get; set; }
        public string Description { get; set; }
        #endregion
        
        public List<TranslationWidgetTemplate> Translations { get; set; }
    }
}