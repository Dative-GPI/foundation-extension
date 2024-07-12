using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IPermissionOrganisationService
    {
        Task<IEnumerable<string>> GetCurrent();
        Task<IEnumerable<PermissionOrganisationInfosViewModel>> GetMany();
        Task<IEnumerable<PermissionOrganisationCategoryViewModel>> GetCategories();
    }
}