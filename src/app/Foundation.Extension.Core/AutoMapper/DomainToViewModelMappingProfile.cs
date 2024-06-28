using AutoMapper;
using AutoMapper.Internal;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.ViewModels;
using System.Linq;

namespace Foundation.Extension.Core.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            InternalApi.Internal(this).ForAllMaps(TranslationMapper.Map);

            CreateMap<RouteInfos, RouteInfosViewModel>();

            CreateMap<PermissionOrganisationInfos, PermissionOrganisationInfosViewModel>();
            CreateMap<PermissionOrganisationCategory, PermissionOrganisationCategoryViewModel>();
            CreateMap<RolePermissionOrganisationDetails, RolePermissionOrganisationDetailsViewModel>()
                .ForMember(vm => vm.PermissionIds, opt => opt.MapFrom(p => p.Permissions.Select(p => p.Id).ToList()));
            CreateMap<ActionInfos, ActionInfosViewModel>();

            CreateMap<UserOrganisationColumn, UserOrganisationColumnViewModel>();
            CreateMap<UserOrganisationTable, UserOrganisationTableViewModel>();
            CreateMap<TranslationColumn, TranslationColumnViewModel>();
            CreateMap<Table, TableViewModel>();
            CreateMap<UserTable, TableViewModel>();


        }
    }
}