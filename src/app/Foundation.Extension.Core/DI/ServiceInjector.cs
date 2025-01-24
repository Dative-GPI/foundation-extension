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
			services.AddScoped<IPermissionOrganisationService, PermissionOrganisationService>();
			services.AddScoped<IRoleOrganisationService, RoleOrganisationService>();
			services.AddScoped<IRoleOrganisationTypeService, RoleOrganisationTypeService>();
			services.AddScoped<IRouteService, RouteService>();
			services.AddScoped<IServiceAccountRoleOrganisationService, ServiceAccountRoleOrganisationService>();
			services.AddScoped<IUserOrganisationTableService, UserOrganisationTableService>();
			services.AddScoped<IUserPermissionOrganisationService, UserPermissionOrganisationService>();

			return services;
		}
	}
}