using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Bones.Flow;

using Foundation.Extension.CrossCutting.DI;

using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.Providers;
using Foundation.Extension.Gateway.Services;
using Foundation.Extension.Gateway.Middlewares;

namespace Foundation.Extension.Gateway.DI
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddGatewayExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddReverseProxy();
            services.AddHttpForwarder();
            services.AddMemoryCache();

            services.AddAuthentication(JWTAuthenticationMiddleware.AuthenticationScheme);

            services.AddAutoMapper(typeof(DependencyInjector).Assembly);

            services.AddCrossCutting(configuration);
            services.AddSingleton<FoundationForwarderMiddleware>();

            services.AddScoped<RequestContextProvider>();
            services.AddScoped<IRequestContextProvider>(sp
                => sp.GetRequiredService<RequestContextProvider>());

            services.AddFlow();

            services.AddAutoMapper(typeof(DependencyInjector).Assembly);

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IApplicationTranslationService, ApplicationTranslationService>();

            services.AddFiles();
            services.AddImages();
            services.AddApplications();
            services.AddApplicationTranslations();

            return services;
        }
    }
}