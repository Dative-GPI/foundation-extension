using AutoMapper;

using Foundation.Extension.Gateway.ViewModels;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Gateway.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ApplicationTranslation, ApplicationTranslationViewModel>();
            CreateMap<ApplicationDetails, ApplicationDetailsViewModel>();
            CreateMap<ImageDetails, ImageDetailsViewModel>();
            CreateMap<FileDetails, FileDetailsViewModel>();
        }
    }
}