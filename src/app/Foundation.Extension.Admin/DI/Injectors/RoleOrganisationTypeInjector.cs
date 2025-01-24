using System;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Admin.Handlers;

namespace Foundation.Extension.Admin.DI
{
    public static class RoleOrganisationTypeInjector
    {
        public static IServiceCollection AddRoleOrganisationTypes(this IServiceCollection services)
        {
            services.AddScoped<RoleOrganisationTypeQueryHandler>();
            services.AddScoped<IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<RoleOrganisationTypeQueryHandler>()
                    .Build();

                return pipeline;
            });


            services.AddScoped<UpdateRoleOrganisationTypeCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRoleOrganisationTypeCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateRoleOrganisationTypeCommand, IEntity<Guid>>()
                    .With<PermissionApplicationsMiddleware>()
                    .Add<UpdateRoleOrganisationTypeCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}