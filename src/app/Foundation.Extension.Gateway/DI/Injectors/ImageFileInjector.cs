using Bones.Flow;

using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Gateway.Handlers;

namespace Foundation.Extension.Gateway.DI
{
    public static class ImageInjector
    {
        public static IServiceCollection AddImages(this IServiceCollection services)
        {
            services.AddScoped<RawImageQueryHandler>();
            services.AddScoped<IQueryHandler<RawImageQuery, byte[]>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<RawImageQuery, byte[]>()
                    .Add<RawImageQueryHandler>()
                    .Build();

                return pipeline;
            });


            services.AddScoped<ThumbnailImageQueryHandler>();
            services.AddScoped<IQueryHandler<ThumbnailImageQuery, byte[]>>(sp =>
            {
                var pipeline = sp.GetPipelineFactory<ThumbnailImageQuery, byte[]>()
                    .Add<ThumbnailImageQueryHandler>()
                    .Build();

                return pipeline;
            });

            return services;
        }
    }
}