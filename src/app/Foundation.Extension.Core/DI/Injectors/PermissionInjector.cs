using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class PermissionInjector
    {
        public static IServiceCollection AddPermissions(this IServiceCollection services)
        {
            services.AddScoped<PermissionCategoriesQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>()
                    .With<PermissionsMiddleware>()
                    .Add<PermissionCategoriesQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<PermissionsQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationsQuery, IEnumerable<PermissionOrganisationInfos>>()
                    .With<PermissionsMiddleware>()
                    .Add<PermissionsQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}