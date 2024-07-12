using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Admin.DI
{
    public static class RoleApplicationInjector
    {
        public static IServiceCollection AddRoleApplication(this IServiceCollection services)
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