using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IEntityPropertyService
    {
        Task<IEnumerable<EntityPropertyViewModel>> GetMany(EntityPropertiesFilterViewModel filter);
    }
}