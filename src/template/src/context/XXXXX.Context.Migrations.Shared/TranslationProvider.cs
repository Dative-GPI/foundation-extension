using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Fixtures;
using System.Linq;

using Foundation.Clients.Fixtures.Services;

namespace XXXXX.Context.Migrations.Shared
{
  public static class TranslationProvider
  {
    static readonly List<string> PROJECTS = new List<string>()
        {
            "../../../src/app/admin/XXXXX.Admin.UI",
            "../../../src/app/core/XXXXX.Core.UI",
        };

    public static async Task<List<Fixture>> GetAllTranslations()
    {
      var translations = new List<Fixture>();
      var fixtureService = new FixtureService();

      foreach (var project in PROJECTS)
      {
        var translation = await TranslationHelper.GetTranslations(project);
        translations.AddRange(translation);
      }

      var translationsCode = fixtureService.GetTranslations().Select(t => t.Code).ToList();

      return translations.DistinctBy(t => t.Code).Where(t => !translationsCode.Contains(t.Code)).ToList();
    }
  }
}
