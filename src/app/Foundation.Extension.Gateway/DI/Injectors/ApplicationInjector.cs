using System;
using Bones.Flow;
using Bones.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Foundation.Extension.Gateway.Handlers;
using Foundation.Extension.Gateway.Requests.Commands;

namespace Foundation.Extension.Gateway.DI
{
    public static class ApplicationInjector
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<CreateApplicationCommandHandler>();
            services.AddScoped<ICommandHandler<CreateApplicationCommand, IEntity<Guid>>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<CreateApplicationCommand, IEntity<Guid>>()
                    .Add<CreateApplicationCommandHandler>()
                    .Build();
            
                return pipeline;
            });

            services.AddScoped<RemoveApplicationCommandHandler>();
            services.AddScoped<ICommandHandler<RemoveApplicationCommand>>(sp => 
            {
                var pipeline = sp.GetPipelineFactory<RemoveApplicationCommand>()
                    .Add<RemoveApplicationCommandHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    } 
}