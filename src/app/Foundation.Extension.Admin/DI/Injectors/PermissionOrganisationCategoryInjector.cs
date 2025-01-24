using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class PermissionOrganisationCategoryInjector
    {
        public static IServiceCollection AddPermissionOrganisationCategories(this IServiceCollection services)
        {
            services.AddScoped<PermissionOrganisationCategoriesQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationCategoriesQuery, IEnumerable<PermissionOrganisationCategory>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<PermissionOrganisationCategoriesQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}