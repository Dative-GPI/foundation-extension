using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Bones.Flow;

using Foundation.Extension.CrossCutting.DI;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Tools;
using Foundation.Extension.Core.Providers;

namespace Foundation.Extension.Core.DI
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddCoreExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DependencyInjector).Assembly);

            services.AddCrossCutting(configuration);

            services.AddScoped<RequestContextProvider>();
            services.AddScoped<IRequestContextProvider>(sp => sp.GetRequiredService<RequestContextProvider>());

            services.AddFlow();
            services.AddServices();
            services.AddMiddlewares();

            services.AddPermissions();
            services.AddRoleOrganisations();
            services.AddRoleOrganisationTypes();
            services.AddServiceAccountRoleOrganisations();
            services.AddTables();
            services.AddUserPermissionOrganisations();

            services.AddScoped<IPermissionProvider, PermissionProvider>();
            services.AddScoped<IApplicationTableProvider, ApplicationTableProvider>();
            services.AddScoped<IOrganisationTypeTableProvider, OrganisationTypeTableProvider>();

            return services;
        }
    }
}