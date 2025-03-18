namespace Foundation.Extension.Domain.Models
{
    public class TranslationWidgetTemplate : ITranslation
    {
        public string LanguageCode { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
    }
}