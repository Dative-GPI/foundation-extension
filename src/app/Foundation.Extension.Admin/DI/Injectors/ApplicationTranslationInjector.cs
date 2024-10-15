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
    public static class ApplicationTranslationInjector
    {
        public static IServiceCollection AddApplicationTranslations(this IServiceCollection services)
        {
            services.AddScoped<ApplicationTranslationsQueryHandler>();
            services.AddScoped<IQueryHandler<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ApplicationTranslationsQuery, IEnumerable<ApplicationTranslation>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<ApplicationTranslationsQueryHandler>()
                    .Build();

                return pipeline;
            });


            services.AddScoped<UpdateApplicationTranslationsCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateApplicationTranslationCommand>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateApplicationTranslationCommand>()
                    .Add<PermissionApplicationsMiddleware>()
                    .Add<UpdateApplicationTranslationsCommandHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<ApplicationTranslationsSpreadSheetQueryHandler>();
            services.AddScoped<IQueryHandler<ApplicationTranslationsSpreadsheetQuery, byte[]>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ApplicationTranslationsSpreadsheetQuery, byte[]>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<ApplicationTranslationsSpreadSheetQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UploadApplicationTranslationsCommandHandler>();
            services.AddScoped<ICommandHandler<UploadApplicationTranslationsCommand>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UploadApplicationTranslationsCommand>()
                    .Add<PermissionApplicationsMiddleware>()
                    .Add<UploadApplicationTranslationsCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}