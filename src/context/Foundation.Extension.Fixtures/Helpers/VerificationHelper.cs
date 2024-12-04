using System;
using System.Linq;
using System.Collections.Generic;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Enums;
using Foundation.Extension.Fixtures;
using Microsoft.Extensions.Logging;
using Foundation.Clients.Fixtures.Services;

namespace Foundation.Extension.Fixtures
{
    public static class VerificationHelper
    {
        public static bool CheckMissingProperties(IEnumerable<EntityProperty> properties, IEnumerable<TranslationDTO> translations, ILogger logger)
        {
            logger.LogInformation("Start looking for missing properties...");

			var fixtureService = new FixtureService();

			properties = properties.Concat(fixtureService.GetEntityProperties().Select(
                p => new EntityProperty()
                {
                    Code = p.Code,
                    EntityType = p.EntityType,
                    TranslationCode = p.TranslationCode,
                }
            ));

            var propertyTranslations = translations.Where(t =>
                t.Code.StartsWith(EntityPropertyHelper.ENTITY_PROPERTY_PREFIX)
                && !Enum.TryParse(t.Code.Split(".")[2], true, out PropertyKind _)
            ).ToList();
            
            //Récupère les translations qui n'ont aucune propriété associée ou héritée
            var missingPropertyTranslations = propertyTranslations.Where(t =>
            {
                return (
                    !properties.Any(p => p.TranslationCode == t.Code)
                    && !(EntityPropertyHelper.IsForeignProperty(t.Code) && EntityPropertyHelper.GetHeritedProperty(t.Code, properties) == null)
                );
            }).ToList();

            if (missingPropertyTranslations.Any())
            {
                foreach (var missingProperty in missingPropertyTranslations)
                {
                    logger.LogWarning("Missing property: {missingProperty}", missingProperty.Code);
                }
                return false;
            }

            return true;
        }

        public static bool CheckDuplicatedTranslations(IEnumerable<TranslationDTO> entities, IEnumerable<TranslationDTO> translations, ILogger logger)
        {
            logger.LogInformation("Start looking for duplicated translations...");

            var duplicatedTranslations = translations.GroupBy(t => t.Code).Where(g => g.Count() > 1).Select(g => g.Key).ToList();

            if (duplicatedTranslations.Any())
            {
                foreach (var duplicatedTranslation in duplicatedTranslations)
                {
                    logger.LogWarning("Duplicated translation: {duplicatedTranslation}", duplicatedTranslation);
                }
                return false;
            }

            return true;
        }

        public static bool CheckDuplicatedDefaultValue(IEnumerable<Translation> entities, IEnumerable<TranslationDTO> translations, ILogger logger)
        {
            logger.LogInformation("Start looking for duplicated default values...");

            var duplicatedDefaultValues = entities.GroupBy(t => t.Value).Where(g => g.Select(t => t.Code).Distinct().Count() > 1).ToList();

            if (duplicatedDefaultValues.Any())
            {
                foreach (var duplicatedTranslation in duplicatedDefaultValues)
                {
                    logger.LogWarning(
                        "Duplicated default values : {duplicatedTranslation} \n    For codes : \n        {codes}", 
                        duplicatedTranslation.Key, 
                        string.Join("\n        ", duplicatedTranslation
                                .Select(t => t.Code)
                                .GroupBy(v => v)
                                .Select(g => $"(x{g.Count()}) {g.Key}")
                        )
                    );
                }
                //Check non bloquant
                return true;
            }

            return true;
        }

        public static bool CheckDuplicatedCodes(IEnumerable<Translation> entities, IEnumerable<TranslationDTO> translations, ILogger logger)
        {
            logger.LogInformation("Start looking for duplicated codes with different default values ...");

            var duplicatedCodes = entities.GroupBy(t => t.Code).Where(g => g.Select(t => t.Value).Distinct().Count() > 1).ToList();

            if (duplicatedCodes.Any())
            {
                foreach (var duplicatedCode in duplicatedCodes)
                {
                    logger.LogWarning(
                        "Duplicated code with differents values : {duplicatedCode} \n    With values : \n        {codes}", 
                        duplicatedCode.Key, 
                        string.Join("\n        ", duplicatedCode
                                .Select(t => t.Value)
                                .GroupBy(v => v)
                                .Select(g => $"(x{g.Count()}) {g.Key}")
                        )
                    );
                }
                //Check non bloquant
                return true;
            }

            return true;
        }
    }
}