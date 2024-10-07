using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Fixtures.Abstractions;
using Microsoft.Extensions.Logging;

namespace Foundation.Extension.Fixtures
{
	public class FixtureHelper : IFixtureHelper
	{
		private ILogger<FixtureHelper> _logger;

		public static string FIXTURES_DIRECTORY = "Fixtures";
		public static string BASE_DIRECTORY = Directory.GetCurrentDirectory();

		public FixtureHelper(ILogger<FixtureHelper> logger)
		{
			_logger = logger;
		}

		public void Feed<TEntity>(ModelBuilder builder) where TEntity : class
		{
			builder.Entity<TEntity>()
				.HasData(Get<TEntity>().Result);
		}

		public async Task<IEnumerable<TEntity>> Get<TEntity>()
		{
			var file = GetPath<TEntity>();
			var data = await ReadAll<TEntity>(file);
			return data;
		}

		public async Task Save<TEntity>(IEnumerable<TEntity> entities)
		{
			var file = GetPath<TEntity>();
			await WriteAll<TEntity>(file, entities);
		}

		private async Task<IEnumerable<TEntity>> ReadAll<TEntity>(string file)
		{
			if (!File.Exists(file))
			{
				_logger.LogWarning($"Unable to find file {file}");
				return Enumerable.Empty<TEntity>();
			}

			IEnumerable<TEntity> result;

			switch (Path.GetExtension(file))
			{
				case ".json":
					result = JsonSerializer.Deserialize<List<TEntity>>(
						await File.ReadAllTextAsync(file)
					);
					break;
				case ".xml":
					result = ReadXML<TEntity>(file);
					break;
				default:
					throw new Exception($"Unsupported file type {Path.GetExtension(file)}");
			}

			return result;
		}

		private async Task WriteAll<TEntity>(string file, IEnumerable<TEntity> entities)
		{
			switch (Path.GetExtension(file))
			{
				case ".json":
					await File.WriteAllTextAsync(
						file,
						JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true })
					);
					break;
				case ".xml":
					WriteXML<TEntity>(file, entities);
					break;
				default:
					throw new Exception($"Unsupported file type {Path.GetExtension(file)}");
			}
		}

		private void WriteXML<TEntity>(string file, IEnumerable<TEntity> entities)
		{
			var serializer = GetSerializer<TEntity>();

			using var stream = File.Create(file);
			serializer.Serialize(stream, entities);
		}

		private IEnumerable<TEntity> ReadXML<TEntity>(string file)
		{
			var serializer = GetSerializer<TEntity>();

			using var stream = File.OpenRead(file);
			return (List<TEntity>)serializer.Deserialize(stream);
		}

		private XmlSerializer GetSerializer<TEntity>()
		{
			XmlAttributeOverrides overrides = new XmlAttributeOverrides();

			Inline(overrides, typeof(TEntity), new HashSet<Type>());

			return new XmlSerializer(typeof(List<TEntity>), overrides);
		}

		public XmlAttributeOverrides Inline(XmlAttributeOverrides overrides, Type type, HashSet<Type> visited)
		{
			if (visited.Contains(type))
			{
				return overrides;
			}

			visited.Add(type);

			foreach (var property in type.GetProperties())
			{
				if (
					property.PropertyType.IsPrimitive ||
					property.PropertyType == typeof(Decimal) ||
					property.PropertyType == typeof(String) ||
					property.PropertyType == typeof(Guid)
				)
				{
					overrides.Add(type, property.Name, new XmlAttributes { XmlAttribute = new XmlAttributeAttribute() });
				}
				else
				{
					if (property.PropertyType.IsGenericType
						&& property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
						&& property.Name.Contains("Translation", StringComparison.OrdinalIgnoreCase)
					)
					{
						foreach (var item in property.PropertyType.GetGenericArguments())
						{
							Inline(overrides, item, visited);
						}
					}
					else
					{
						// ignore property
						overrides.Add(type, property.Name, new XmlAttributes { XmlIgnore = true });
					}

					// if (property.PropertyType.IsGenericType)
					// {
					//     foreach (var item in property.PropertyType.GetGenericArguments())
					//     {
					//         Inline(overrides, item, visited);
					//     }
					// }
					// else
					// {
					//     Inline(overrides, property.PropertyType, visited);
					// }
				}
			}

			return overrides;
		}

		private string GetPath<TEntity>()
		{
			// TODO create directory
			if (Directory.Exists(Path.Join(BASE_DIRECTORY, FIXTURES_DIRECTORY)) == false)
			{
				Directory.CreateDirectory(Path.Join(BASE_DIRECTORY, FIXTURES_DIRECTORY));
			}

			var baseName = GetFileBaseName<TEntity>();
			var files = Directory.EnumerateFiles(Path.Join(BASE_DIRECTORY, FIXTURES_DIRECTORY), $"{baseName}.*");

			if (files.Count() > 1)
			{
				_logger.LogError("Multiple files found for {file} taking first one : [{files}]", baseName, files);
				return files.First();
			}

			if (files.Any())
			{
				return files.First();
			}

			return Path.Join(BASE_DIRECTORY, FIXTURES_DIRECTORY, $"{baseName}.xml");
		}

		private string GetFileBaseName<TEntity>()
		{
			return PascalToKebabCase(typeof(TEntity).Name);
		}

		private string PascalToKebabCase(string value)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			return Regex.Replace(
				value,
				"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z0-9])",
				"-$1",
				RegexOptions.Compiled)
				.Trim()
				.ToLower();
		}
	}
}
