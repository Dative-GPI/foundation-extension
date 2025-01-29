using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class ServiceAccountPermissionOrganisationInjector
    {
        public static IServiceCollection AddServiceAccountOrganisations(this IServiceCollection services)
        {
            services.AddScoped<ServiceAccountOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<ServiceAccountOrganisationQuery, ServiceAccountOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ServiceAccountOrganisationQuery, ServiceAccountOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<ServiceAccountOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}