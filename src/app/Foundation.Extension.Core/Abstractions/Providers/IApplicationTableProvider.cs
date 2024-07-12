using System;
using System.Threading.Tasks;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IApplicationTableProvider
    {
        Task<ApplicationTableDetails> Get(Guid tableId);
    }
}