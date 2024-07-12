using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<PermissionOrganisationCategory, PermissionOrganisationCategoryViewModel>();
      CreateMap<PermissionOrganisationDetails, PermissionOrganisationDetailsViewModel>();
      CreateMap<PermissionOrganisationInfos, PermissionOrganisationInfosViewModel>();
      CreateMap<PermissionOrganisationTypeInfos, PermissionOrganisationTypeInfosViewModel>()
          .ForMember(p => p.PermissionLabel, opt => opt.MapFromTranslation(t => t.TranslationPermissions, t => t.Label));

      CreateMap<PermissionApplicationCategory, PermissionApplicationCategoryViewModel>();
      CreateMap<PermissionApplicationDetails, PermissionApplicationDetailsViewModel>();
      CreateMap<PermissionApplicationInfos, PermissionApplicationInfosViewModel>();

      CreateMap<RouteInfos, RouteInfosViewModel>();

      CreateMap<Translation, TranslationViewModel>();
      CreateMap<ApplicationTranslation, ApplicationTranslationViewModel>();

      CreateMap<RoleApplicationDetails, RoleApplicationDetailsViewModel>()
          .ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(r => r.Permissions.Select(p => p.Id).ToList()));

      CreateMap<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>()
          .ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(r => r.Permissions.Select(p => p.Id).ToList()));

      CreateMap<Page, PageViewModel>();

      CreateMap<EntityProperty, EntityPropertyViewModel>();
      CreateMap<EntityPropertyTranslation, EntityPropertyTranslationViewModel>();

      #region Tables
      CreateMap<ApplicationTableInfos, ApplicationTableInfosViewModel>();
      CreateMap<ApplicationTableDetails, ApplicationTableDetailsViewModel>();
      CreateMap<OrganisationTypeColumnInfos, OrganisationTypeColumnInfosViewModel>();
      CreateMap<OrganisationTypeTableDetails, OrganisationTypeTableDetailsViewModel>();
      CreateMap<OrganisationTypeTableInfos, OrganisationTypeTableInfosViewModel>();
      CreateMap<Column, ColumnViewModel>();
      CreateMap<TranslationItemProperty, TranslationColumnViewModel>();
      #endregion

    }
  }
}