using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.Gateway.Middlewares
{
    public class ClaimsToHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ClaimsToHeadersMiddleware> _logger;
        private IServiceProvider _sp;

        public ClaimsToHeadersMiddleware(RequestDelegate next, 
            ILogger<ClaimsToHeadersMiddleware> logger,
            IServiceProvider sp)
        {
            _next = next;
            _logger = logger;
            _sp = sp;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogTrace("Invoked");

            using var scope = _sp.CreateScope();
            var claimFactory = scope.ServiceProvider.GetRequiredService<IClaimsFactory>();
            // Clean headers potentially used
            context.Request.Headers.Remove("X-User-Id");
            context.Request.Headers.Remove("X-Source-Id");
            context.Request.Headers.Remove("X-Application-Id");

            if (context.GetEndpoint()?.Metadata.Any(m => m is AllowAnonymousAttribute) ?? false)
            {
                _logger.LogTrace("Anonymous request");
                await _next(context);
                return;
            }

            var claims = claimFactory.Get(context.User.Claims);

            context.Request.Headers.Add("X-Application-Id", claims.ApplicationId.ToString());

            if (claims.UserId.HasValue)
                context.Request.Headers.Add("X-User-Id", claims.UserId.ToString());

            if (claims.SourceId.HasValue)
                context.Request.Headers.Add("X-Source-Id", claims.SourceId.ToString());

            await _next(context);
        }
    }
}