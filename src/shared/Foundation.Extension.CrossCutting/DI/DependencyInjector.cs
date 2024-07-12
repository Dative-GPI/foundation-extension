using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Foundation.Clients.DI;

using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.CrossCutting.Services;

namespace Foundation.Extension.CrossCutting.DI
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddCrossCutting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFoundationClients(configuration);

            services.AddScoped<IClaimsFactory, ClaimsFactory>();
            services.AddScoped<IFoundationClientFactory, FoundationClientFactory>();
            services.AddScoped<ITranslationsProvider, TranslationsProvider>();
            services.AddScoped<IExtensionMatcher, RouteExtensionMatcher>();

            return services;
        }
    }
}