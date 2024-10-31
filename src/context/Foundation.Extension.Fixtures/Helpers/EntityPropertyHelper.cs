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
								EntityType = g.Key.ToString().Kebaberize().Pascalize(),
								LabelDefault = p.Name.Humanize(),
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

		private static string GetEntityPropertyParentId(string entityPropertyCode)
		{
			var fixtureService = new FixtureService();
			var entityPropertiesCode = fixtureService.GetEntityProperties();

			var result = entityPropertiesCode.SingleOrDefault(e => e.Code.ToLower() == entityPropertyCode.ToLower())?.Id;
			return result?.ToString();
		}


		private static string GetTranslationCode(string type, PropertyInfo property)
		{
			if(property.IsDefined(typeof(StandardPropertyAttribute), false))
			{
				var attribute = property.GetCustomAttribute<StandardPropertyAttribute>();
				return $"entity.common.{attribute.PropertyKind.ToString().Kebaberize()}";
			}
			else if(property.IsDefined(typeof(CustomPropertyAttribute), false))
			{
				return $"entity.{type.ToString().Kebaberize()}.{property.Name.Kebaberize()}";
			}
			else if(property.IsDefined(typeof(StandardForeignPropertyAttribute), false))
			{
				var attribute = property.GetCustomAttribute<StandardForeignPropertyAttribute>();
				return $"entity.{attribute.Owner.ToString().Kebaberize()}.{attribute.PropertyKind.ToString().Kebaberize()}.foreign";
			}
			else if(property.IsDefined(typeof(CustomForeignPropertyAttribute), false))
			{
				var attribute = property.GetCustomAttribute<CustomForeignPropertyAttribute>();
				return $"entity.{attribute.Owner.ToString().Kebaberize()}.{attribute.PropertyName.Kebaberize()}.foreign";
			}
			else
			{
				return null;
			}
			
		}
	}
}