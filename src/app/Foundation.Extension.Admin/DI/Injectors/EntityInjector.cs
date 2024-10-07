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
            services.AddEntityPropertyTranslations();

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

        private static IServiceCollection AddEntityPropertyTranslations(this IServiceCollection services)
        {
            services.AddScoped<EntityPropertyTranslationsQueryHandler>();
            services.AddScoped<IQueryHandler<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyApplicationTranslation>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<EntityPropertyTranslationsQuery, IEnumerable<EntityPropertyApplicationTranslation>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<EntityPropertyTranslationsQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<ReplaceEntityPropertyTranslationsCommandHandler>();
            services.AddScoped<ICommandHandler<ReplaceEntityPropertyTranslationsCommand>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ReplaceEntityPropertyTranslationsCommand>()
                    .Add<PermissionApplicationsMiddleware>()
                    .Add<ReplaceEntityPropertyTranslationsCommandHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<EntityPropertyTranslationsSpreadsheetQueryHandler>();
            services.AddScoped<IQueryHandler<EntityPropertyTranslationsSpreadsheetQuery, byte[]>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<EntityPropertyTranslationsSpreadsheetQuery, byte[]>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<EntityPropertyTranslationsSpreadsheetQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UploadEntityPropertyTranslationsCommandHandler>();
            services.AddScoped<ICommandHandler<UploadEntityPropertyTranslationsCommand>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UploadEntityPropertyTranslationsCommand>()
                    .Add<PermissionApplicationsMiddleware>()
                    .Add<UploadEntityPropertyTranslationsCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}