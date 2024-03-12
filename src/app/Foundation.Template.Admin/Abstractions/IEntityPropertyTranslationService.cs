using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Template.Admin.ViewModels;

namespace Foundation.Template.Admin.Abstractions
{
    public interface IEntityPropertyTranslationService
    {
        Task<IEnumerable<EntityPropertyTranslationViewModel>> GetMany(EntityPropertyTranslationsFilterViewModel filter);
        Task<IEnumerable<EntityPropertyTranslationViewModel>> Replace(Guid entityPropertyId, List<UpdateEntityPropertyTranslationViewModel> payload);
        Task<byte[]> Download();
        Task<IEnumerable<EntityPropertyTranslationViewModel>> Upload(List<SpreadsheetColumnDefinitionViewModel> labels, List<SpreadsheetColumnDefinitionViewModel> categories, Stream file);
    }
}