using System;
using System.Threading.Tasks;

using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Abstractions
{
    public interface IFileService
    {
        Task<FileDetailsViewModel> Get(Guid id);
    }
}