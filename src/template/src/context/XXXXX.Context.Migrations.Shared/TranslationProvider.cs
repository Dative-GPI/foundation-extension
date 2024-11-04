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

			var properties = await EntityPropertyProvider.GetAllEntityProperties();
			var propertyTranslations = properties.Select(p => new Translation
			{
				Code = p.TranslationCode,
				Value = p.LabelDefault,
				Context = p.Context
			});

			var translationsCode = fixtureService.GetTranslations().Select(t => t.Code).ToList();

            return translations.Concat(propertyTranslations)
                .DistinctBy(t => t.Code)
				.Where(t => !translationsCode.Contains(t.Code))
                .ToList();
		}
	}
}
