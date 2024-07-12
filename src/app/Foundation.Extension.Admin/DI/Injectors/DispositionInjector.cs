using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Foundation.Extension.Admin.Handlers;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.DI
{
    public static class DispositionInjector
    {
        public static IServiceCollection AddDispositions(this IServiceCollection services)
        {
            services.AddTables();
            services.AddColumns();
            services.AddOrganisationTypeDispositions();

            return services;
        }

        private static IServiceCollection AddTables(this IServiceCollection services)
        {
            services.AddScoped<TablesQueryHandler>();
            services.AddScoped<IQueryHandler<TablesQuery, IEnumerable<ApplicationTableInfos>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<TablesQuery, IEnumerable<ApplicationTableInfos>>()
                    // .With<PermissionsMiddleware>()
                    .Add<TablesQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<TableQueryHandler>();
            services.AddScoped<IQueryHandler<TableQuery, ApplicationTableDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<TableQuery, ApplicationTableDetails>()
                    // .With<PermissionsMiddleware>()
                    .Add<TableQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<PatchTableCommandHandler>();
            services.AddScoped<ICommandHandler<PatchTableCommand>>(sp => 
            {
                var pipeline = sp.GetPipelineFactory<PatchTableCommand>()
                    // .Add<PermissionsMiddleware>()
                    .Add<PatchTableCommandHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateTableCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateTableCommand>>(sp => 
            {
                var pipeline = sp.GetPipelineFactory<UpdateTableCommand>()
                    // .Add<PermissionsMiddleware>()
                    .Add<UpdateTableCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }

        private static IServiceCollection AddColumns(this IServiceCollection services)
        {

            services.AddScoped<ColumnsQueryHandler>();
            services.AddScoped<IQueryHandler<ColumnsQuery, IEnumerable<Column>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ColumnsQuery, IEnumerable<Column>>()
                    // .With<PermissionsMiddleware>()
                    .Add<ColumnsQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }


        private static IServiceCollection AddOrganisationTypeDispositions(this IServiceCollection services)
        {

            services.AddScoped<OrganisationTypeTableQueryHandler>();
            services.AddScoped<IQueryHandler<OrganisationTypeTableQuery, OrganisationTypeTableDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<OrganisationTypeTableQuery, OrganisationTypeTableDetails>()
                    // .With<PermissionsMiddleware>()
                    .Add<OrganisationTypeTableQueryHandler>()
                    .Build();

                return pipeline;
            });

            services.AddScoped<UpdateColumnOrganisationTypesCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateOrganisationTypeTableCommand>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<UpdateOrganisationTypeTableCommand>()
                    // .Add<PermissionsMiddleware>()
                    .Add<UpdateColumnOrganisationTypesCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}