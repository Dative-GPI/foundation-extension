using System;
using System.Runtime.Intrinsics.Arm;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Fixtures;
using Foundation.Extension.Fixtures.Abstractions;

using Microsoft.Extensions.Logging;

namespace XXXXX.Context.Migrations
{
  public class FixtureManager : BaseFixtureManager
  {
    public FixtureManager(ILogger<FixtureManager> logger, IFixtureHelper helper) : base(logger, helper)
    {
      Add<TranslationDTO, Fixture>(
          TranslationProvider.GetAllTranslations,
          fixture => new TranslationDTO()
          {
            Id = Guid.NewGuid(),
            Code = fixture.Code,
            ValueDefault = fixture.Value
          },
          (fixture, dto) =>
          {
            dto.ValueDefault = fixture.Value;
            return dto;
          });

      Add<TableDTO, Fixture>(
          TableProvider.GetAllTables,
          fixture => new TableDTO()
          {
            Id = Guid.NewGuid(),
            Code = fixture.Code,
            EntityType = fixture.Value
          },
          (fixture, dto) =>
          {
            dto.EntityType = fixture.Value;
            return dto;
          });

      Add<PermissionOrganisationDTO, Fixture>(
          PermissionHelper.GetPermissions(typeof(XXXXX.Core.Kernel.Authorizations)),
          fixture => new PermissionOrganisationDTO()
          {
            Id = Guid.NewGuid(),
            Code = fixture.Code,
            LabelDefault = fixture.Value
          },
          (fixture, dto) =>
          {
            dto.LabelDefault = fixture.Value;
            return dto;
          });

      Add<PermissionOrganisationCategoryDTO, Fixture>(
          PermissionHelper.GetCategories(typeof(XXXXX.Core.Kernel.Authorizations)),
          fixture => new PermissionOrganisationCategoryDTO()
          {
            Id = Guid.NewGuid(),
            Code = fixture.Code,
            Prefix = fixture.Code + ".",
            LabelDefault = fixture.Value
          },
          (fixture, dto) =>
          {
            dto.LabelDefault = fixture.Value;
            return dto;
          });

      Add<PermissionApplicationDTO, Fixture>(
          PermissionHelper.GetPermissions(typeof(XXXXX.Admin.Kernel.Authorizations)),
          fixture => new PermissionApplicationDTO()
          {
            Id = Guid.NewGuid(),
            Code = fixture.Code,
            LabelDefault = fixture.Value
          },
          (fixture, dto) =>
          {
            dto.LabelDefault = fixture.Value;
            return dto;
          });

      Add<PermissionApplicationCategoryDTO, Fixture>(
          PermissionHelper.GetCategories(typeof(XXXXX.Admin.Kernel.Authorizations)),
          fixture => new PermissionApplicationCategoryDTO()
          {
            Id = Guid.NewGuid(),
            Code = fixture.Code,
            Prefix = fixture.Code + ".",
            LabelDefault = fixture.Value
          },
          (fixture, dto) =>
          {
            dto.LabelDefault = fixture.Value;
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
            ParentId = fixture.ParentId
          },
          (prop, dto) =>
          {
            dto.EntityType = prop.EntityType;
            dto.LabelDefault = prop.LabelDefault;
            dto.Value = prop.Value;
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
}