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
			"../../../src/app/admin/XXXXX.Admin.UI/src",
			"../../../src/app/core/XXXXX.Core.UI/src",
		};

        public static async Task<List<Translation>> GetAllDistinctTranslations()
        {
            var translations = new List<Translation>();
			var fixtureService = new FixtureService();

            foreach (var project in PROJECTS)
            {
                var translation = await TranslationHelper.GetDistinctTranslations(project);
                translations.AddRange(translation);
            }


            var allTranslations = await GetTranslations(translations);

			var foundationTranslationCodes = fixtureService.GetTranslations().Select(t => t.Code).ToList();

            return allTranslations.DistinctBy(t => t.Code)
				.Where(t => !foundationTranslationCodes.Contains(t.Code))
                .ToList();
        }

        public static async Task<List<Translation>> GetAllTranslations()
        {
            var localTranslations = new List<Translation>();

            foreach (var project in PROJECTS)
            {
                var translation = await TranslationHelper.GetTranslations(project);
                localTranslations.AddRange(translation);
            }

            return await GetTranslations(localTranslations);
        }

        private static async Task<List<Translation>> GetTranslations(List<Translation> localTranslations)
        {
            var properties = await EntityPropertyProvider.GetAllEntityProperties();
            var propertyTranslations = properties.Select(p =>
            {
                var isForeign = EntityPropertyHelper.IsForeignProperty(p.TranslationCode);
                var heritedProperty = isForeign ? p.GetHeritedProperty(properties) : null;

                return new Translation
                {
                    Code = p.TranslationCode,
                    Value = p.LabelDefault ?? (isForeign && heritedProperty != null ? $"{heritedProperty.EntityType} {heritedProperty.LabelDefault}" : null),
                    Context = isForeign && heritedProperty != null ? heritedProperty.Context : p.Context
                };
            });
            
            //Get all actions translations
			localTranslations.AddRange(
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

			localTranslations.AddRange(
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

            //Concat propertyTranslations first to override existing translations if property context has been updated
            return propertyTranslations
                .Concat(localTranslations)
                .ToList();
        }
	}
}
