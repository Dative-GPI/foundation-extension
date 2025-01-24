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
			CreateMap<ActionInfos, ActionInfosViewModel>();

			CreateMap<ApplicationTableDetails, ApplicationTableDetailsViewModel>();
			CreateMap<ApplicationTableInfos, ApplicationTableInfosViewModel>();
			CreateMap<Column, ColumnViewModel>();
			CreateMap<OrganisationTypeColumnInfos, OrganisationTypeColumnInfosViewModel>();
			CreateMap<OrganisationTypeTableDetails, OrganisationTypeTableDetailsViewModel>();
			CreateMap<OrganisationTypeTableInfos, OrganisationTypeTableInfosViewModel>();
			CreateMap<TranslationItemProperty, TranslationColumnViewModel>();

			CreateMap<EntityPropertyApplicationTranslation, EntityPropertyTranslationViewModel>();
			CreateMap<EntityProperty, EntityPropertyViewModel>();

			CreateMap<Page, PageViewModel>();

			CreateMap<PermissionApplicationDetails, PermissionApplicationDetailsViewModel>();
			CreateMap<PermissionApplicationInfos, PermissionApplicationInfosViewModel>();
			CreateMap<PermissionApplicationCategory, PermissionApplicationCategoryViewModel>();

			CreateMap<PermissionOrganisationCategory, PermissionOrganisationCategoryViewModel>();
			CreateMap<PermissionOrganisationDetails, PermissionOrganisationDetailsViewModel>();
			CreateMap<PermissionOrganisationInfos, PermissionOrganisationInfosViewModel>();
			CreateMap<PermissionOrganisationTypeInfos, PermissionOrganisationTypeInfosViewModel>()
				.ForMember(p => p.PermissionLabel, opt => opt.MapFromTranslation(t => t.TranslationPermissions, t => t.Label));

			CreateMap<RoleApplicationDetails, RoleApplicationDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(r => r.Permissions.Select(p => p.Id).ToList()));

			CreateMap<RoleOrganisationTypeDetails, RoleOrganisationTypeDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(r => r.Permissions.Select(p => p.Id).ToList()));

			CreateMap<RouteInfos, RouteInfosViewModel>();

			CreateMap<ApplicationTranslation, ApplicationTranslationViewModel>();
			CreateMap<Translation, TranslationViewModel>();
		}
	}
}