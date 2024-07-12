using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface ITableService
    {
        Task<ApplicationTableDetailsViewModel> Get(Guid tableId);
        Task<IEnumerable<ApplicationTableInfosViewModel>> GetMany(TableFiltersViewModel filter);
        Task<ApplicationTableDetailsViewModel>  Patch(Guid tableId);
        Task<ApplicationTableDetailsViewModel> Update(Guid tableId, UpdateTableViewModel payload);
    }
}