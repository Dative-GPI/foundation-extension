using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class MiddlewareInjector
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<PermissionApplicationsMiddleware>();
            
            return services;
        }
    }
}