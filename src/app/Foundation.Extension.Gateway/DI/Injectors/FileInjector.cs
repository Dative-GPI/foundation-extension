using Bones.Flow;
using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Gateway.Handlers;

namespace Foundation.Extension.Gateway.DI
{
    public static class FileInjector
    {
        public static IServiceCollection AddFiles(this IServiceCollection services)
        {
            services.AddScoped<FileQueryHandler>();
            services.AddScoped<IQueryHandler<FileQuery, FileDetails>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<FileQuery, FileDetails>()
                    .Add<FileQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}