using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Foundation.Extension.Fixtures
{
    public static class TranslationHelper
    {
        static string[] FILES_PATTERN = new[] { ".vue", ".ts" };
        const string REGEX_PATTERN = @"\$tr\(\s*['""]([\w\.-]*)['""],\s*(?:[']([^']*)[']|[""]([^""]*)[""])\s*(?:,\s*[^)]+)?\s*\)";
        static Regex Regex = new Regex(REGEX_PATTERN);

        public static async Task<List<Translation>> GetTranslations(params string[] projects)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var project in projects)
            {
                var files = Directory.GetFiles(project, "*", SearchOption.AllDirectories)
                    .Where(filepath => FILES_PATTERN.Any(pattern => filepath.EndsWith(pattern)));

                foreach (var filepath in files)
                {
                    var content = await File.ReadAllTextAsync(filepath);
                    var matches = Regex.Matches(content);

                    foreach (Match match in matches)
                    {
                        var translationKey = match.Groups[1].Value;
                        var translationValue = match.Groups[2].Value + match.Groups[3].Value; // Group2 = xxx in 'xxx', Group3 = xxx in "xxx"

                        var translationAdded = result.TryAdd(translationKey, translationValue);

                        if (!translationAdded)
                        {
                            var existingValue = result.ContainsKey(translationKey) ? result[translationKey] : null;

                            if (translationValue == existingValue) continue;
                        }
                    }
                }
            }

            return result
                .Select(kv => new Translation()
                {
                    Code = kv.Key,
                    Value = kv.Value
                })
                .ToList();
        }
    }
}