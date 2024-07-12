using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Gateway.Middlewares;

using Microsoft.AspNetCore.Routing;

namespace Foundation.Extension.Gateway.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExtensionAuthentication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<JWTAuthenticationMiddleware>();
            builder.UseMiddleware<ClaimsToHeadersMiddleware>();
            builder.UseMiddleware<RequestContextInitializerMiddleware>();

            return builder;
        }

        public static IEndpointRouteBuilder MapGatewayExtensionEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var foundationForwarder = endpoints.ServiceProvider.GetRequiredService<FoundationForwarderMiddleware>();

            endpoints.MapControllers();
            endpoints.Map("/api/foundation/{**catch-all}", foundationForwarder.Forward);

            return endpoints;
        }
    }
}