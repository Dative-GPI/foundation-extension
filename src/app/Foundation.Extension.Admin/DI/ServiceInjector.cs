using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Admin.Services;
using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.DI
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IActionService, ActionService>();

            services.AddScoped<IPageService, PageService>();

            services.AddScoped<IPermissionOrganisationTypeService, PermissionOrganisationTypeService>();
            services.AddScoped<IPermissionOrganisationService, PermissionOrganisationService>();
            services.AddScoped<IPermissionOrganisationCategoryService, PermissionOrganisationCategoryService>();
            services.AddScoped<IRolePermissionOrganisationService, RolePermissionOrganisationService>();

            services.AddScoped<IPermissionApplicationService, PermissionApplicationService>();
            services.AddScoped<IPermissionApplicationCategoryService, PermissionApplicationCategoryService>();
            services.AddScoped<IRoleApplicationService, RoleApplicationService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IEntityPropertyService, EntityPropertyService>();
            services.AddScoped<IEntityPropertyTranslationService, EntityPropertyTranslationService>();
            services.AddScoped<IOrganisationTypeTableService, OrganisationTypeTableService>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<IApplicationTranslationService, ApplicationTranslationService>();

            return services;
        }
    }
}