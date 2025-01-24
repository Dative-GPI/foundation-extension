using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class PermissionInjector
    {
        public static IServiceCollection AddPermissionOrganisations(this IServiceCollection services)
        {
            services.AddScoped<PermissionOrganisationCategoriesQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>()
                    .With<PermissionsMiddleware>()
                    .Add<PermissionOrganisationCategoriesQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<PermissionOrganisationsQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>()
                    .With<PermissionsMiddleware>()
                    .Add<PermissionOrganisationsQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}