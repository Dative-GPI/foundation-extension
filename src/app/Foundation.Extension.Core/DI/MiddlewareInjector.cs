using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class MiddlewareInjector
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<PermissionsMiddleware>();
            
            return services;
        }
    }
}