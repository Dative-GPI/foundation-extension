using Microsoft.Extensions.DependencyInjection;

using Foundation.Template.Shell.Services;
using Foundation.Template.Shell.Abstractions;

namespace Foundation.Template.Shell.DI
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IActionService, ActionService>();
            
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleOrganisationService, RoleOrganisationService>();

            return services;
        }
    }
}