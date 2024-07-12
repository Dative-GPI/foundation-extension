using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Admin.DI
{
    public static class RolePermissionOrganisationInjector
    {
        public static IServiceCollection AddRolePermissionOrganisation(this IServiceCollection services)
        {
            services.AddScoped<RolePermissionOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RolePermissionOrganisationQuery, RolePermissionOrganisationDetails>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<RolePermissionOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });


            services.AddScoped<UpdateRolePermissionOrganisationCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRolePermissionOrganisationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateRolePermissionOrganisationCommand, IEntity<Guid>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<UpdateRolePermissionOrganisationCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}