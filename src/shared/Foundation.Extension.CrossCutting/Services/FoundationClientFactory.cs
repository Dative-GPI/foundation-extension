using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Clients.Abstractions;

namespace Foundation.Extension.CrossCutting.Services
{
    public class FoundationClientFactory : IFoundationClientFactory
    {
        private IServiceProvider _sp;
        private IApplicationRepository _applicationRepository;

        public FoundationClientFactory(
            IServiceProvider sp,
            IApplicationRepository applicationRepository
        )
        {
            _sp = sp;
            _applicationRepository = applicationRepository;
        }

        public async Task<IFoundationClient> CreateAnonymous(Guid applicationId, string languageCode)
        {
            var client = _sp.GetRequiredService<IFoundationClient>();

            var app = await _applicationRepository.Get(applicationId);

            client.Init(app.Host, languageCode, null);

            return client;
        }
        
        public async Task<IFoundationClient> CreateAuthenticated(Guid applicationId, string languageCode, string jwt)
        {
            var client = _sp.GetRequiredService<IFoundationClient>();

            var app = await _applicationRepository.Get(applicationId);

            client.Init(app.Host, languageCode, jwt);

            return client;
        }
        
        public async Task<IFoundationClient> CreateAdmin(Guid applicationId, string languageCode)
        {
            var client = _sp.GetRequiredService<IFoundationClient>();

            var app = await _applicationRepository.Get(applicationId);

            client.Init(app.Host, languageCode, app.AdminJWT);

            return client;
        }
    }
}