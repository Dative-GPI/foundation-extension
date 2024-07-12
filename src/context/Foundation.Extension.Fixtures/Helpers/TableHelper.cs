using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Foundation.Extension.Fixtures
{
    public static class TableHelper
    {
        const string FILES_PATTERN = "tables.ts";

        // de la forme "***" ou avec simple quote
        // [^'] = tout sauf simple quote (utilisé à l'intérieur d'un bloc de simple quote), pareil pour les doubles
        const string REGEX_PATTERN = @"(?:[']([^']*)[']|[""]([^""]*)[""])";

        // static readonly List<string> TablesFiles = new () {
        //     "../../../app/admin/Foundation.Extension.Admin-UI/config/literals/tables.ts",
        //     "../../../app/admin/Foundation.Extension.Core-UI/config/literals/tables.ts"
        // };

        static readonly Regex Regex = new(REGEX_PATTERN);

        public static async Task<List<Fixture>> GetTables(params string[] projects)
        {
            HashSet<string> result = new HashSet<string>();

            foreach (var project in projects)
            {
                var files = Directory.GetFiles(project, FILES_PATTERN, SearchOption.AllDirectories)
                    .Where(filepath => FILES_PATTERN.Any(pattern => filepath.EndsWith(pattern)));

                foreach (var filepath in files)
                {
                    var content = await File.ReadAllTextAsync(filepath);
                    var matches = Regex.Matches(content);
                    foreach (Match match in matches)
                    {
                        result.Add(match.Groups[1].Value + match.Groups[2].Value);
                    }
                }

            }

            return result
                .Select(table => new Fixture()
                {
                    Code = table,
                    Value = "A REMPLIR MANUELLEMENT"
                })
                .ToList();
        }
    }
}