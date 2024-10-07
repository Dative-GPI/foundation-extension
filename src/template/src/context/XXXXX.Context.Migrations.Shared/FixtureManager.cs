using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Fixtures;
using Foundation.Extension.Fixtures.Abstractions;

using Microsoft.Extensions.Logging;

namespace XXXXX.Context.Migrations.Shared
{
	public class FixtureManager : BaseFixtureManager
	{
		static string[] DEFAULT_LANGUAGES = new string[] { "en-GB", "fr-FR", "es-ES", "it-IT", "de-DE" };
		public FixtureManager(ILogger<FixtureManager> logger, IFixtureHelper helper) : base(logger, helper)
		{
			Add<TranslationDTO, Fixture>(
				TranslationProvider.GetAllTranslations,
				fixture => new TranslationDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					ValueDefault = fixture.Value,
					Translations = DEFAULT_LANGUAGES.Select(l => new TranslationTranslationDTO()
					{
						LanguageCode = l,
						Value = null
					}).ToList()
				},
				(fixture, dto) =>
				{
					dto.ValueDefault = fixture.Value;
					return dto;
				});

			Add<TableDTO, Table>(
				TableProvider.GetAllTables,
				fixture => new TableDTO()
				{
                    Id = Guid.NewGuid(),
                    Code = fixture.Code,
                    LabelDefault = fixture.LabelDefault,
                    EntityType = fixture.EntityType
				},
				(fixture, dto) =>
				{
					// dto.EntityType = fixture.Value;
					return dto;
				});

			Add<PermissionOrganisationDTO, Fixture>(
				PermissionHelper.GetPermissions(typeof(XXXXX.Core.Kernel.Authorizations)),
				fixture => new PermissionOrganisationDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					LabelDefault = fixture.Value,
					Translations = DEFAULT_LANGUAGES.Select(l => new TranslationPermissionOrganisationDTO()
					{
						LanguageCode = l,
						Label = null
					}).ToList()
				},
				(fixture, dto) =>
				{
                    dto.Translations = dto.Translations
                        .Concat(DEFAULT_LANGUAGES
                            .Select(l => new TranslationPermissionOrganisationDTO()
                            {
                                LanguageCode = l,
                                Label = null
                            })
                        )
                        .DistinctBy(t => t.LanguageCode)
                        .ToList();
					return dto;
				});

			Add<PermissionOrganisationCategoryDTO, Fixture>(
				PermissionHelper.GetCategories(typeof(XXXXX.Core.Kernel.Authorizations)),
				fixture => new PermissionOrganisationCategoryDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					Prefix = fixture.Code + ".",
					LabelDefault = fixture.Value,
                    Translations = DEFAULT_LANGUAGES.Select(l => new TranslationPermissionOrganisationCategoryDTO()
                    {
                        LanguageCode = l,
                        Label = null
                    }).ToList()
				},
				(fixture, dto) =>
				{
                    dto.Translations = dto.Translations
                        .Concat(DEFAULT_LANGUAGES
                            .Select(l => new TranslationPermissionOrganisationCategoryDTO()
                            {
                                LanguageCode = l,
                                Label = null
                            })
                        )
                        .DistinctBy(t => t.LanguageCode)
                        .ToList();
					return dto;
				});

			Add<PermissionApplicationDTO, Fixture>(
				PermissionHelper.GetPermissions(typeof(XXXXX.Admin.Kernel.Authorizations)),
				fixture => new PermissionApplicationDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					LabelDefault = fixture.Value,
                    Translations = DEFAULT_LANGUAGES.Select(l => new TranslationPermissionApplicationDTO()
                    {
                        LanguageCode = l,
                        Label = null
                    }).ToList()
				},
				(fixture, dto) =>
				{
                    dto.Translations = dto.Translations
                        .Concat(DEFAULT_LANGUAGES
                            .Select(l => new TranslationPermissionApplicationDTO()
                            {
                                LanguageCode = l,
                                Label = null
                            })
                        )
                        .DistinctBy(t => t.LanguageCode)
                        .ToList();
					return dto;
				});

			Add<PermissionApplicationCategoryDTO, Fixture>(
				PermissionHelper.GetCategories(typeof(XXXXX.Admin.Kernel.Authorizations)),
				fixture => new PermissionApplicationCategoryDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					Prefix = fixture.Code + ".",
					LabelDefault = fixture.Value,
                    Translations = DEFAULT_LANGUAGES.Select(l => new TranslationPermissionApplicationCategoryDTO()
                    {
                        LanguageCode = l,
                        Label = null
                    }).ToList()
				},
				(fixture, dto) =>
				{
                    dto.Translations = dto.Translations
                        .Concat(DEFAULT_LANGUAGES
                            .Select(l => new TranslationPermissionApplicationCategoryDTO()
                            {
                                LanguageCode = l,
                                Label = null
                            })
                        )
                        .DistinctBy(t => t.LanguageCode)
                        .ToList();
					return dto;
				});

			Add<EntityPropertyDTO, EntityProperty>(
				EntityPropertyProvider.GetAllEntityProperties,
				fixture => new EntityPropertyDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					EntityType = fixture.EntityType,
					LabelDefault = fixture.LabelDefault,
					Value = fixture.Value,
					ParentId = fixture.ParentId,
                    Translations = DEFAULT_LANGUAGES.Select(l => new TranslationEntityPropertyDTO()
                    {
                        LanguageCode = l,
                        Label = null
                    }).ToList()
				},
				(prop, dto) =>
				{
					dto.EntityType = prop.EntityType;
					dto.Value = prop.Value;
                    dto.Translations = dto.Translations
                        .Concat(DEFAULT_LANGUAGES
                            .Select(l => new TranslationEntityPropertyDTO()
                            {
                                LanguageCode = l,
                                Label = null
                            })
                        )
                        .DistinctBy(t => t.LanguageCode)
                        .ToList();
					return dto;
				});

			Add<PageDTO, Page>(
				PageProvider.GetAllPages,
				fixture => new PageDTO()
				{
					Id = Guid.NewGuid(),
					Code = fixture.Code,
					LabelDefault = fixture.LabelDefault,
					ShowOnDrawer = fixture.ShowOnDrawer
				},
				(prop, dto) =>
				{
					dto.LabelDefault = prop.LabelDefault;
					dto.ShowOnDrawer = prop.ShowOnDrawer;
					return dto;
				});
		}
	}
}
