using System;

namespace Foundation.Extension.Domain.Models
{
	public class TranslationItemProperty : ITranslation
	{
		public string LanguageCode { get; set; }
		public string Label { get; set; }
	}
}