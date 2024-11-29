using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Humanizer;

using Foundation.Clients.Fixtures.Services;
using Foundation.Extension.Domain.Attributes;

namespace Foundation.Extension.Fixtures
{
	public static class EntityPropertyHelper
	{
		public const string ENTITY_INFOS_PATTERN = "InfosViewModel";
		public const string ENTITY_DETAILS_PATTERN = "DetailsViewModel";

        public static readonly string FOREIGN_SUFFIX = ".foreign";
        public static readonly string ENTITY_PROPERTY_PREFIX = "entity.";

		public static List<EntityProperty> GetAllEntities(List<Assembly> assemblies, List<string> namespaces)
		{
			var result = assemblies.SelectMany(a => a.GetTypes())
				.Where(t => namespaces.Any(n => t.Namespace?.StartsWith(n) ?? false))
				.Where(t => t.IsDefined(typeof(EntityDescriptionAttribute), false))
				.GroupBy(t => t.GetCustomAttribute<EntityDescriptionAttribute>().EntityType)
				.SelectMany(g =>
					g.SelectMany(t =>
						t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
							.Where(p => p.IsDefined(typeof(StandardPropertyAttribute), false)
								|| p.IsDefined(typeof(CustomPropertyAttribute), false)
								|| p.IsDefined(typeof(StandardForeignPropertyAttribute), false)
								|| p.IsDefined(typeof(CustomForeignPropertyAttribute), false)
							)
							.Select(p => new EntityProperty()
							{
								Code = $"entity.{g.Key.ToString().Kebaberize()}.{p.Name.Kebaberize()}",
								Value = p.Name.Camelize(),
								EntityType = g.Key.ToString(),
                                LabelDefault = p.GetCustomAttribute<DefaultTranslationAttribute>()?.DefaultTranslation,
								Context = p.GetCustomAttribute<CustomPropertyAttribute>()?.Description,
								TranslationCode = GetTranslationCode(g.Key, p),
								EntityKind = t.GetCustomAttribute<EntityDescriptionAttribute>().EntityKind,
								ParentId = GetEntityPropertyParentId(p.Name)
							})
					)
				)
				.Where(e => !e.Code.EndsWith("id") || e.Code.EndsWith("image-id"))
				.DistinctBy(e => e.Code)
				.ToList();

			return result;
		}

        public static bool IsForeignProperty(this string translationCode)
        {
            return translationCode.EndsWith(FOREIGN_SUFFIX);
        }

        public static EntityProperty GetHeritedProperty(string translationCode, IEnumerable<EntityProperty> properties)
        {
            return properties.FirstOrDefault(p => p.TranslationCode == translationCode[..^FOREIGN_SUFFIX.Length].ToString());
        }

        public static EntityProperty GetHeritedProperty(this EntityProperty translationCode, List<EntityProperty> properties) => GetHeritedProperty(translationCode.TranslationCode, properties);

		private static string GetEntityPropertyParentId(string entityPropertyCode)
		{
			var fixtureService = new FixtureService();
			var entityPropertiesCode = fixtureService.GetEntityProperties();

			var result = entityPropertiesCode.SingleOrDefault(e => e.Code.ToLower() == entityPropertyCode.ToLower())?.Id;
			return result?.ToString();
		}

		private static string GetTranslationCode(string type, PropertyInfo property)
        {
            if (property.IsDefined(typeof(StandardPropertyAttribute), false))
            {
                var attribute = property.GetCustomAttribute<StandardPropertyAttribute>();
                return $"{ENTITY_PROPERTY_PREFIX}common.{attribute.PropertyKind.ToString().Kebaberize()}";
            }
            else if (property.IsDefined(typeof(CustomPropertyAttribute), false))
            {
                return $"{ENTITY_PROPERTY_PREFIX}{type.ToString().Kebaberize()}.{property.Name.Kebaberize()}";
            }
            else if (property.IsDefined(typeof(StandardForeignPropertyAttribute), false))
            {
                var attribute = property.GetCustomAttribute<StandardForeignPropertyAttribute>();
                return $"{ENTITY_PROPERTY_PREFIX}{attribute.Owner.ToString().Kebaberize()}.{attribute.PropertyKind.ToString().Kebaberize()}{FOREIGN_SUFFIX}";
            }
            else if (property.IsDefined(typeof(CustomForeignPropertyAttribute), false))
            {
                var attribute = property.GetCustomAttribute<CustomForeignPropertyAttribute>();
                return $"{ENTITY_PROPERTY_PREFIX}{attribute.Owner.ToString().Kebaberize()}.{attribute.PropertyName.Kebaberize()}{FOREIGN_SUFFIX}";
            }
            else
            {
                return null;
            }

        }
	}
}