using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class PermissionOrganisationTypeInjector
    {
        public static IServiceCollection AddPermissionOrganisationTypes(this IServiceCollection services)
        {
            services.AddScoped<PermissionOrganisationTypesQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionOrganisationTypesQuery, IEnumerable<PermissionOrganisationTypeInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionOrganisationTypesQuery, IEnumerable<PermissionOrganisationTypeInfos>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<PermissionOrganisationTypesQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpsertPermissionOrganisationTypesCommandHandler>();
            services.AddScoped<ICommandHandler<UpsertPermissionOrganisationTypesCommand>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpsertPermissionOrganisationTypesCommand>()
                    .Add<PermissionApplicationsMiddleware>()
                    .Add<UpsertPermissionOrganisationTypesCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}