using System;
using System.Threading.Tasks;
using Foundation.Clients.Abstractions;

namespace Foundation.Extension.Domain.Abstractions
{
    public interface IFoundationClientFactory
    {
        Task<IFoundationClient> CreateAnonymous(Guid applicationId, string languageCode = null);
        Task<IFoundationClient> CreateAuthenticated(Guid applicationId, string languageCode, string jwt);
        Task<IFoundationClient> CreateAdmin(Guid applicationId, string languageCode = null);
    }
}