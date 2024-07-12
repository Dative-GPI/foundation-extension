using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using static Foundation.Extension.Fixtures.BaseFixtureManager;

namespace Foundation.Extension.Fixtures
{
    public static class PermissionHelper
    {
        private const BindingFlags PublicStaticFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        public static FixtureGenerator<Fixture> GetPermissions(Type type)
        {
            var permissions = GetAllPermissions(type);
            return () => Task.FromResult(permissions);
        }

        public static FixtureGenerator<Fixture> GetCategories(Type type)
        {
            var permissions = GetAllPermissions(type);
            var categories = ExtractCategories(permissions);

            return () => Task.FromResult(categories);
        }

        private static List<Fixture> ExtractCategories(List<Fixture> permissions)
        {
            return permissions
                .Select(p => new { Code = String.Join('.', p.Code.Split(".")[0..^1]), Value = p.Value })
                .GroupBy(p => p.Code)
                .Select(kv => new Fixture()
                {
                    Code = kv.Key,
                    Value = kv.SelectMany(p => p.Value.Split(new char[] { ' ', ',', '.', '_' }, StringSplitOptions.RemoveEmptyEntries))
                        .GroupBy(word => word)
                        .OrderBy(g => g.Count() + g.Key.Length)
                        .LastOrDefault()?.Key
                })
                .ToList();
        }

        private static List<Fixture> GetAllPermissions(Type type)
        {
            return type.GetFields(PublicStaticFlags)
                .Where(field => field.IsLiteral && !field.IsInitOnly)
                .Select(
                    field => new Fixture()
                    {
                        Code = field.GetRawConstantValue()?.ToString(),
                        Value = field.Name,
                    }
                ).ToList();
        }
    }
}