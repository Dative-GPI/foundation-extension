using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class PermissionApplicationInjector
    {
        public static IServiceCollection AddPermissionApplications(this IServiceCollection services)
        {
            services.AddScoped<PermissionApplicationsQueryHandler>();
            services.AddScoped<IQueryHandler<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<PermissionApplicationsQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}