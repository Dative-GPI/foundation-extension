using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IPermissionOrganisationTypeService
    {
        Task<IEnumerable<PermissionOrganisationTypeInfosViewModel>> GetMany(PermissionOrganisationTypesFilterViewModel filter);
        Task Upsert(List<UpsertPermissionOrganisationTypesViewModel> payload);
    }
}