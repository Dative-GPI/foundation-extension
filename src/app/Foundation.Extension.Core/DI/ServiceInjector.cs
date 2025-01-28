using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Core.Services;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.DI
{
	public static class ServiceInjector
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IActionService, ActionService>();
			services.AddScoped<IPermissionOrganisationCategoryService, PermissionOrganisationCategoryService>();
			services.AddScoped<IPermissionOrganisationService, PermissionOrganisationService>();
			services.AddScoped<IRoleOrganisationService, RoleOrganisationService>();
			services.AddScoped<IRoleOrganisationTypeService, RoleOrganisationTypeService>();
			services.AddScoped<IRouteService, RouteService>();
			services.AddScoped<IServiceAccountOrganisationService, ServiceAccountOrganisationService>();
			services.AddScoped<IServiceAccountRoleOrganisationService, ServiceAccountRoleOrganisationService>();
			services.AddScoped<IUserOrganisationService, UserOrganisationService>();
			services.AddScoped<IUserOrganisationTableService, UserOrganisationTableService>();

			return services;
		}
	}
}