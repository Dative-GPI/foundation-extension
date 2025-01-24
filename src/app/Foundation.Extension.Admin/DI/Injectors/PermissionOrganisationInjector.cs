using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class PermissionOrganisationInjector
    {
        public static IServiceCollection AddPermissionOrganisations(this IServiceCollection services)
        {
            services.AddScoped<PermissionOrganisationsQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<PermissionOrganisationsQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}