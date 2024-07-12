using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;

using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Forwarder;

using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Gateway.Middlewares
{
    public class FoundationForwarderMiddleware
    {
        private ILogger<FoundationForwarderMiddleware> _logger;
        private IHttpForwarder _forwarder;
        private HttpMessageInvoker _httpClient;
        private readonly IMemoryCache _memoryCache;

        static MemoryCacheEntryOptions _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        public FoundationForwarderMiddleware(
            ILogger<FoundationForwarderMiddleware> logger,
            IHttpForwarder forwarder,
            IForwarderHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _forwarder = forwarder;
            _httpClient = httpClientFactory.CreateClient(new ForwarderHttpClientContext()
            {
                NewConfig = HttpClientConfig.Empty with { DangerousAcceptAnyServerCertificate = true }
            });

            _memoryCache = memoryCache;
        }

        public async Task Forward(HttpContext httpContext)
        {
            using var scope = httpContext.RequestServices.CreateScope();

            var sp = scope.ServiceProvider;

            var applicationId = Guid.Parse(
                httpContext.Request.Headers["X-Application-Id"].ToString()
            );

            if (!_memoryCache.TryGetValue(applicationId, out string host))
            {
                _logger.LogTrace("MemoryCache does not contain the host, fetching it from the database.");
                
                var applicationRepository = httpContext.RequestServices.GetRequiredService<IApplicationRepository>();
                var application = await applicationRepository.Get(applicationId);

                if (application == null)
                {
                    _logger.LogWarning("No application found with id {0}, can't forward the request.", applicationId);
                    httpContext.Response.StatusCode = 400;
                    return;
                }

                host = new UriBuilder("https", application.Host).Uri.ToString();

                _memoryCache.Set(applicationId, host, _cacheEntryOptions);
            }

            _logger.LogTrace("Forwarding to {host}", host);

            await _forwarder.SendAsync(httpContext, host, _httpClient);
        }
    }
}