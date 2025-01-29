using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Admin.Services;
using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.DI
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<IApplicationTranslationService, ApplicationTranslationService>();
            services.AddScoped<IEntityPropertyService, EntityPropertyService>();
            services.AddScoped<IOrganisationTypeTableService, OrganisationTypeTableService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IPermissionApplicationCategoryService, PermissionApplicationCategoryService>();
            services.AddScoped<IPermissionApplicationService, PermissionApplicationService>();
            services.AddScoped<IPermissionOrganisationCategoryService, PermissionOrganisationCategoryService>();
            services.AddScoped<IPermissionOrganisationService, PermissionOrganisationService>();
            services.AddScoped<IPermissionOrganisationTypeService, PermissionOrganisationTypeService>();
            services.AddScoped<IRoleApplicationService, RoleApplicationService>();
            services.AddScoped<IRoleOrganisationTypeService, RoleOrganisationTypeService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ITranslationService, TranslationService>();

            return services;
        }
    }
}