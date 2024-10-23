using System.Linq;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.CrossCutting.Helpers
{
	public static class TranslationsHelper
	{
		public static string GetTranslationValue(string languageCode, Translation defaultTranslation, IEnumerable<ApplicationTranslation> appTranslations)
		{
			var result = appTranslations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Value;

			if (result == null)
			{
				result = defaultTranslation.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Value;
			}

			if (result == null)
			{
				result = defaultTranslation.Value;
			}

			return result;
		}

		public static string GetTranslationValue(string languageCode, EntityProperty entityProperty, IEnumerable<EntityPropertyApplicationTranslation> appTranslations)
		{
			var result = appTranslations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Label;

			if (result == null)
			{
				result = entityProperty.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Label;
			}

			if (result == null)
			{
				result = entityProperty.LabelDefault;
			}

			return result;
		}
	}
}