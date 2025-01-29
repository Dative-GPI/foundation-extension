using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class PermissionOrganisationCategoryInjector
    {
        public static IServiceCollection AddPermissionOrganisationCategories(this IServiceCollection services)
        {
            services.AddScoped<PermissionOrganisationCategoriesQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategoryInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategoryInfos>>()
                    .With<PermissionsMiddleware>()
                    .Add<PermissionOrganisationCategoriesQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}