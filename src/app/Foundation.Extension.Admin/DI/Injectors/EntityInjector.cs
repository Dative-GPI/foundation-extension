using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Admin.Handlers;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.DI
{
    public static class EntityInjector
    {
        public static IServiceCollection AddEntities(this IServiceCollection services)
        {
            services.AddScoped<EntityPropertiesQueryHandler>();
            services.AddScoped<IQueryHandler<EntityPropertiesQuery, IEnumerable<EntityProperty>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<EntityPropertiesQuery, IEnumerable<EntityProperty>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<EntityPropertiesQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}