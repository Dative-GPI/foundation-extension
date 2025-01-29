using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class UserPermissionOrganisationInjector
    {
        public static IServiceCollection AddUserOrganisations(this IServiceCollection services)
        {
            services.AddScoped<CurrentUserOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<CurrentUserOrganisationQuery, UserOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<CurrentUserOrganisationQuery, UserOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<CurrentUserOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UserOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<UserOrganisationQuery, UserOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UserOrganisationQuery, UserOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<UserOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}