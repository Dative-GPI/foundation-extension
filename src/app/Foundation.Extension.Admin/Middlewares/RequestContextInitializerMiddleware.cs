using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Admin.Providers;
using Foundation.Extension.Admin.Models;

namespace Foundation.Extension.Admin.Middlewares
{
    public class RequestContextInitializerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var provider = context.RequestServices.GetRequiredService<RequestContextProvider>();

            var actorId = new Guid(request.Headers["X-User-Id"].ToString());
            var applicationId = new Guid(request.Headers["X-Application-Id"].ToString());
            var languageCode = request.Headers["X-Language-Code"].ToString();
            var isAuthenticated = request.Headers.ContainsKey(HeaderNames.Authorization);
            var jwt = isAuthenticated ? request.Headers[HeaderNames.Authorization].ToString().Substring(7) : null;

            provider.Context = new RequestContext()
            {
                ApplicationId = applicationId,
                ActorId = actorId,
                LanguageCode = languageCode,
                Jwt = jwt
            };

            await _next(context);
        }
    }
}