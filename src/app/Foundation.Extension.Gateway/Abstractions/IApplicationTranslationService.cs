using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Abstractions
{
    public interface IApplicationTranslationService
    {
        Task<IEnumerable<ApplicationTranslationViewModel>> GetMany();
    }
}