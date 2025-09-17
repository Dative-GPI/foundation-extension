using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Foundation.Extension.Domain.Extensions
{
    public static class SearchVectorExtensions
    {
        public static string RemoveDiacritics(this string value)
        {
            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString.EnumerateRunes())
            {
                var unicodeCategory = Rune.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string FormatForTsQuery(this string search)
        {
            var words = search.RemoveDiacritics()
                .Replace("\\", " ").Replace("&", " ").Replace("|", " ").Replace(":", " ").Replace("*", " ")
                .Replace("'", " ").Replace("(", " ").Replace(")", " ").Replace("!", " ").Replace("@", " ").Replace("<", " ")
                .Replace(">", " ")
                .Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w));
                
            if (!words.Any())
            {
                return string.Empty;
            }
            return $"{string.Join(":* & ", words)}:*";
        }
    }
}