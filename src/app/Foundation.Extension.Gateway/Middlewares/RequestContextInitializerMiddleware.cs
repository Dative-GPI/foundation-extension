using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Foundation.Extension.Gateway.Providers;
using Foundation.Extension.Gateway.Models;

namespace Foundation.Extension.Gateway.Middlewares
{
    public class RequestContextInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestContextInitializerMiddleware> _logger;

        public RequestContextInitializerMiddleware(RequestDelegate next,
            ILogger<RequestContextInitializerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogTrace("Invoked");

            if (context.GetEndpoint()?.Metadata.Any(m => m is AllowAnonymousAttribute) ?? false)
            {
                _logger.LogTrace("Anonymous request");
                await _next(context);
                return;
            }

            var request = context.Request;
            var provider = context.RequestServices.GetRequiredService<RequestContextProvider>();

            Guid? actorId = null;
            Guid? sourceId = null;

            if (request.Headers.ContainsKey("X-User-Id"))
                actorId = new Guid(request.Headers["X-User-Id"].ToString());

            if (request.Headers.ContainsKey("X-Source-Id"))
                sourceId = new Guid(request.Headers["X-Source-Id"].ToString());

            var applicationId = new Guid(request.Headers["X-Application-Id"].ToString());
            var languageCode = request.Headers["X-Language-Code"].ToString();

            var isAuthenticated = request.Headers.ContainsKey(HeaderNames.Authorization);
            var jwt = isAuthenticated ? request.Headers[HeaderNames.Authorization].ToString().Substring(7) : null;

            provider.Context = new RequestContext()
            {
                ApplicationId = applicationId,
                ActorId = actorId,
                SourceId = sourceId,
                LanguageCode = languageCode,

                Jwt = jwt
            };

            await _next(context);
        }
    }
}