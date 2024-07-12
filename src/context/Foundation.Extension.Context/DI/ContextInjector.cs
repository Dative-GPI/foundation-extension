using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
    }
}