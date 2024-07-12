using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IActionService
    {
        Task<IEnumerable<ActionInfosViewModel>> GetMany(ActionsFilterViewModel filter);
    }
}
