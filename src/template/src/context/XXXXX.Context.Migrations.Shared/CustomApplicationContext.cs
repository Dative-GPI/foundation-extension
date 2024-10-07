using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Fixtures.Abstractions;

using XXXXX.Context.Kernel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;

namespace XXXXX.Context.Migrations.Shared
{
	public class CustomApplicationContext : ApplicationContext
	{
		private ILogger<CustomApplicationContext> _logger;
		private IFixtureHelper _fixtureHelper;

		public CustomApplicationContext(
			ILogger<CustomApplicationContext> logger,
			DbContextOptions<CustomApplicationContext> options,
			IFixtureHelper fixtureHelper) : base(new DbContextOptions<ApplicationContext>(
				options.Extensions.ToDictionary(e => e.GetType()))
			)
		{
			_logger = logger;
			_fixtureHelper = fixtureHelper;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			_fixtureHelper.Feed<PermissionOrganisationDTO>(modelBuilder);
			_fixtureHelper.Feed<PermissionOrganisationCategoryDTO>(modelBuilder);
			_fixtureHelper.Feed<PermissionApplicationDTO>(modelBuilder);
			_fixtureHelper.Feed<PermissionApplicationCategoryDTO>(modelBuilder);
			_fixtureHelper.Feed<TranslationDTO>(modelBuilder);
			_fixtureHelper.Feed<TableDTO>(modelBuilder);
			_fixtureHelper.Feed<EntityPropertyDTO>(modelBuilder);
			_fixtureHelper.Feed<PageDTO>(modelBuilder);

			ConfigureJsonColumn<PermissionOrganisationDTO, TranslationPermissionOrganisationDTO>(modelBuilder, p => p.Translations);
			ConfigureJsonColumn<PermissionOrganisationCategoryDTO, TranslationPermissionOrganisationCategoryDTO>(modelBuilder, p => p.Translations);
			ConfigureJsonColumn<PermissionApplicationDTO, TranslationPermissionApplicationDTO>(modelBuilder, p => p.Translations);
			ConfigureJsonColumn<PermissionApplicationCategoryDTO, TranslationPermissionApplicationCategoryDTO>(modelBuilder, p => p.Translations);
			ConfigureJsonColumn<EntityPropertyDTO, TranslationEntityPropertyDTO>(modelBuilder, e => e.Translations);
			ConfigureJsonColumn<TranslationDTO, TranslationTranslationDTO>(modelBuilder, t => t.Translations);
		}

		private void ConfigureJsonColumn<TSource, TTranslation>(ModelBuilder modelBuilder, Expression<Func<TSource, List<TTranslation>>> propertyExpression)
			where TSource : class
		{
			// seule solution pour que ça marche bien dans les fixtures

			modelBuilder.Entity<TSource>()
				.Property(propertyExpression)
				.HasConversion(
					v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
					v => JsonSerializer.Deserialize<List<TTranslation>>(v, null as JsonSerializerOptions)
				);
		}
	}
}