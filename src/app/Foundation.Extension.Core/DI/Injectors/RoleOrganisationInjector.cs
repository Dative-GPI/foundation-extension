using System;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class RoleOrganisationInjector
    {
        public static IServiceCollection AddRoleOrganisations(this IServiceCollection services)
        {
            services.AddScoped<RoleOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<RoleOrganisationQuery, RoleOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RoleOrganisationQuery, RoleOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<RoleOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateRoleOrganisationCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRoleOrganisationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateRoleOrganisationCommand, IEntity<Guid>>()
                    .With<PermissionsMiddleware>()
                    .Add<UpdateRoleOrganisationCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}