using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Humanizer;

using Foundation.Clients.Fixtures.Services;

namespace Foundation.Extension.Fixtures
{
    public static class EntityPropertyHelper
    {
        public const string ENTITY_INFOS_PATTERN = "InfosViewModel";
        public const string ENTITY_DETAILS_PATTERN = "DetailsViewModel";

        public static List<EntityProperty> GetAll(List<Assembly> assemblies, List<string> namespaces)
        {
            var result = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => namespaces.Any(n => t.Namespace?.StartsWith(n) ?? false))
                .Where(t => t.Name.EndsWith(ENTITY_INFOS_PATTERN) || t.Name.EndsWith(ENTITY_DETAILS_PATTERN))
                .GroupBy(t => t.Name.Replace(ENTITY_DETAILS_PATTERN, "").Replace(ENTITY_INFOS_PATTERN, ""))
                .SelectMany(g => 
                    g.SelectMany(t =>
                        t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                            .Select(p => new EntityProperty()
                            {
                                Code = $"entity.{g.Key.Kebaberize().Replace("-organisation-type", "").Replace("-organisation", "").Replace("-shallow", "")}.{p.Name.Kebaberize().Replace("-organisation-type", "").Replace("-organisation", "").Replace("-shallow", "")}",
                                Value = p.Name.Camelize(),
                                EntityType = g.Key.Kebaberize().Replace("-organisation-type", "").Replace("-organisation", "").Replace("-shallow", "").Replace("-", " ").Pascalize(),
                                LabelDefault = p.Name.Humanize(),
                                ParentId = GetEntityPropertyParentId(p.Name)
                            })
                    )
                )
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
    }
}