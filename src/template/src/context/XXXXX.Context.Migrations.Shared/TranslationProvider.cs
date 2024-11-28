using System.Threading.Tasks;
using System.Collections.Generic;

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

		public static async Task<List<Translation>> GetAllTranslations()
		{
			var translations = new List<Translation>();
			var fixtureService = new FixtureService();

            foreach (var project in PROJECTS)
            {
                var translation = await TranslationHelper.GetTranslations(project);
                translations.AddRange(translation);
            }

			translations.AddRange(
                XXXXX.Core.Kernel.Actions.GetActions()
                .SelectMany(a => new List<Translation>()
                {
                    new Translation()
                    {
                        Code = a.LabelCode,
                        Value = a.LabelDefault,
                        Context = a.LabelDefault
                    }
                })
            );

			translations.AddRange(
                XXXXX.Admin.Kernel.Actions.GetActions()
                .SelectMany(a => new List<Translation>()
                {
                    new Translation()
                    {
                        Code = a.LabelCode,
                        Value = a.LabelDefault,
                        Context = a.LabelDefault
                    }
                })
            );

			var foundationTranslationCodes = fixtureService.GetTranslations().Select(t => t.Code).ToList();

            return translations
                .DistinctBy(t => t.Code)
				.Where(t => !foundationTranslationCodes.Contains(t.Code))
                .ToList();
		}
	}
}
