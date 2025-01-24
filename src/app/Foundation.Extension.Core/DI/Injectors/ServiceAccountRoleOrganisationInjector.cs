using System;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class ServiceAccountRoleOrganisationInjector
    {
        public static IServiceCollection AddServiceAccountRoleOrganisations(this IServiceCollection services)
        {
            services.AddScoped<ServiceAccountRoleOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<ServiceAccountRoleOrganisationQuery, ServiceAccountRoleOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ServiceAccountRoleOrganisationQuery, ServiceAccountRoleOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<ServiceAccountRoleOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateServiceAccountRoleOrganisationCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateServiceAccountRoleOrganisationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateServiceAccountRoleOrganisationCommand, IEntity<Guid>>()
                    .With<PermissionsMiddleware>()
                    .Add<UpdateServiceAccountRoleOrganisationCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}