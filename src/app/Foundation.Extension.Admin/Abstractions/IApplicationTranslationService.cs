using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IApplicationTranslationService
    {
        Task<IEnumerable<ApplicationTranslationViewModel>> GetMany(ApplicationTranslationViewModel filter);
        Task<IEnumerable<ApplicationTranslationViewModel>> Update(string code, UpdateApplicationTranslationViewModel payload);
        Task<byte[]> Download();
        Task<IEnumerable<ApplicationTranslationViewModel>> Upload(IEnumerable<SpreadsheetColumnDefinitionViewModel> languages, Stream file);
    }
}