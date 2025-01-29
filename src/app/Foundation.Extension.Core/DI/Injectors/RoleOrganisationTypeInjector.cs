using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Core.Handlers;

namespace Foundation.Extension.Core.DI
{
    public static class RoleOrganisationTypeInjector
    {
        public static IServiceCollection AddRoleOrganisationTypes(this IServiceCollection services)
        {
            services.AddScoped<RoleOrganisationTypeQueryHandler>();
            services.AddScoped<IQueryHandler<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RoleOrganisationTypeQuery, RoleOrganisationTypeDetails>()
                    .With<PermissionsMiddleware>()
                    .Add<RoleOrganisationTypeQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}