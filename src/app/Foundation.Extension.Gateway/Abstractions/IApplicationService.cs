using System;
using System.Threading.Tasks;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Abstractions
{
    public interface IApplicationService
    {
        Task<ApplicationDetailsViewModel> Create(CreateApplicationViewModel payload);
        Task Remove(Guid applicationId);
    }
}