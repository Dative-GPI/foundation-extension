using System;
using System.Threading.Tasks;
using Foundation.Extension.Admin.Models;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IApplicationTableProvider
    {
        Task<ApplicationTableDetails> Get(Guid tableId);
    }
}