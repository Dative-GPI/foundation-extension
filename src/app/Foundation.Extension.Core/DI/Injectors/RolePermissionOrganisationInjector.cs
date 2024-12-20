using System;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class RolePermissionOrganisationInjector
    {
        public static IServiceCollection AddRolePermissionOrganisations(this IServiceCollection services)
        {
            services.AddScoped<ServiceAccountRoleOrganisationPermissionOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<ServiceAccountRoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ServiceAccountRoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<ServiceAccountRoleOrganisationPermissionOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<RoleOrganisationTypePermissionOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<RoleOrganisationTypePermissionOrganisationQuery, RolePermissionOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RoleOrganisationTypePermissionOrganisationQuery, RolePermissionOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<RoleOrganisationTypePermissionOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<RoleOrganisationPermissionOrganisationQueryHandler>();
            services.AddScoped<IQueryHandler<RoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RoleOrganisationPermissionOrganisationQuery, RolePermissionOrganisationDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<RoleOrganisationPermissionOrganisationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateServiceAccountRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateServiceAccountRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>>()
                    .With<PermissionsMiddleware>()
                    .Add<UpdateServiceAccountRoleOrganisationPermissionOrganisationCommandHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateRoleOrganisationPermissionOrganisationCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateRoleOrganisationPermissionOrganisationCommand, IEntity<Guid>>()
                    .With<PermissionsMiddleware>()
                    .Add<UpdateRoleOrganisationPermissionOrganisationCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}