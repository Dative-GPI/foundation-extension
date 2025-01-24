using System;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class RoleApplicationInjector
    {
        public static IServiceCollection AddRoleApplications(this IServiceCollection services)
        {
            services.AddScoped<RoleApplicationQueryHandler>();
            services.AddScoped<IQueryHandler<RoleApplicationQuery, RoleApplicationDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RoleApplicationQuery, RoleApplicationDetails>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<RoleApplicationQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateRoleApplicationCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRoleApplicationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateRoleApplicationCommand, IEntity<Guid>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<UpdateRoleApplicationCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}