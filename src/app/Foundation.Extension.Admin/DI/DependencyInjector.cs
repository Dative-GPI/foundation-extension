using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Bones.Flow;

using Foundation.Extension.CrossCutting.DI;

using Foundation.Extension.Admin.Providers;
using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.DI
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddAdminExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DependencyInjector).Assembly);

            services.AddCrossCutting(configuration);

            services.AddScoped<RequestContextProvider>();
            services.AddScoped<IRequestContextProvider>(sp
                => sp.GetRequiredService<RequestContextProvider>());

            services.AddFlow();
            services.AddServices();
            services.AddMiddlewares();

            services.AddPermissions();
            services.AddPermissionOrganisationCategories();
            services.AddPermissionOrganisationTypes();
            services.AddRolePermissionOrganisation();

            services.AddPermissionApplications();
            services.AddPermissionApplicationCategories();
            services.AddRoleApplication();

            services.AddTranslations();
            services.AddApplicationTranslations();
            services.AddDispositions();
            services.AddEntities();
            services.AddPages();

            services.AddScoped<IPermissionProvider, PermissionProvider>();
            services.AddScoped<IApplicationTableProvider, ApplicationTableProvider>();
            services.AddScoped<IOrganisationTypeTableProvider, OrganisationTypeTableProvider>();

            return services;
        }
    }
}