using System;
using System.Collections.Generic;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;
using Foundation.Extension.Admin.Requests;

namespace Foundation.Extension.Admin.DI
{
    public static class TranslationInjector
    {
        public static IServiceCollection AddTranslations(this IServiceCollection services)
        {
            services.AddScoped<TranslationsQueryHandler>();

            services.AddScoped<IQueryHandler<TranslationsQuery, IEnumerable<Translation>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<TranslationsQuery, IEnumerable<Translation>>()
                    .With<PermissionApplicationsMiddleware>()
					.Add<TranslationsQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}