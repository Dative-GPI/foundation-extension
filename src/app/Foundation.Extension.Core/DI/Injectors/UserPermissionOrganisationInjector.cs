using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class UserPermissionOrganisationInjector
    {
        public static IServiceCollection AddUserPermissionOrganisations(this IServiceCollection services)
        {
            services.AddScoped<UserPermissionOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<UserPermissionOrganisationQuery, UserPermissionOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UserPermissionOrganisationQuery, UserPermissionOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<UserPermissionOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}