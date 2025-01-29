using System.Linq;

using AutoMapper;
using AutoMapper.Internal;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.AutoMapper
{
	public class DomainToViewModelMappingProfile : Profile
	{
		public DomainToViewModelMappingProfile()
		{
			InternalApi.Internal(this).ForAllMaps(TranslationMapper.Map);

			CreateMap<ActionInfos, ActionInfosViewModel>();

			CreateMap<PathCrumb, PathCrumbViewModel>();

			CreateMap<PermissionOrganisationCategoryInfos, PermissionOrganisationCategoryInfosViewModel>();
			CreateMap<PermissionOrganisationInfos, PermissionOrganisationInfosViewModel>();

			CreateMap<RoleOrganisationDetails, RoleOrganisationDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(p => p.Permissions.Select(p => p.Id).ToList()));

			CreateMap<RoleOrganisationTypeDetails, RoleOrganisationTypeDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(p => p.Permissions.Select(p => p.Id).ToList()));

			CreateMap<RouteInfos, RouteInfosViewModel>();

			CreateMap<ServiceAccountOrganisationDetails, ServiceAccountOrganisationDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(p => p.Permissions.Select(p => p.Id).ToList()));

			CreateMap<ServiceAccountRoleOrganisationDetails, ServiceAccountRoleOrganisationDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(p => p.Permissions.Select(p => p.Id).ToList()));

			CreateMap<CompleteUserOrganisationColumnInfos, UserOrganisationColumnInfosViewModel>();

			CreateMap<UserOrganisationDetails, UserOrganisationDetailsViewModel>()
				.ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(p => p.Permissions.Select(p => p.Id).ToList()));

			CreateMap<UserOrganisationTableDetails, UserOrganisationTableDetailsViewModel>();
		}
	}
}