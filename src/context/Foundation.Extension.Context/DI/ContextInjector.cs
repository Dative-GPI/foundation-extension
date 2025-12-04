using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Azure.Security.KeyVault.Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using Bones.Flow;

using Foundation.Extension.Context.Configurations;
using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Context.Services;

namespace Foundation.Extension.Context.DI
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddContextExtension<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : BaseApplicationContext
        {
            services.Configure<FileConfiguration>(configuration.GetSection("Files"));

            services.AddRepositories();
            services.AddVault(configuration);

            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PGSQL"));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<BaseApplicationContext>(sp => sp.GetRequiredService<TContext>());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<IBinaryStorage, BinaryStorage>();

            return services;
        }

        private static IServiceCollection AddVault(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CertificateClient>(sp =>
            {
                var connstr = configuration.GetConnectionString("AZURE_KEY_VAULT");
                return new CertificateClient(new Uri(connstr), new DefaultAzureCredential());
            });

            services.AddScoped<SecretClient>(sp => {            
                var connstr = configuration.GetConnectionString("AZURE_KEY_VAULT");
                return new SecretClient(new Uri(connstr), new DefaultAzureCredential());
            });

            return services;
        }
    }
}