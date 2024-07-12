using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

using Foundation.Extension.Fixtures;

namespace XXXXX.Context.Migrations
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var ServiceProvider = GetServiceProvider();

      var logger = ServiceProvider.GetRequiredService<ILogger<Program>>();

      if (EF.IsDesignTime)
      {
        logger.LogInformation("Design time detected");
        return;
      }

      if (args.Contains("fixtures"))
      {
        var fixtureManager = ServiceProvider.GetRequiredService<FixtureManager>();
        if (args.Contains("generate"))
        {
          await fixtureManager.Generate(args.Contains("--dry-run"));
          return;
        }
        if (args.Contains("list"))
        {
          await fixtureManager.List();
          return;
        }
        logger.LogError("Command unknown {command}", args);
        return;
      }

      logger.LogInformation("Applying migrations");

      IEnumerable<DbContext> contexts = new List<DbContext>()
            {
                ServiceProvider.GetRequiredService<CustomApplicationContext>()
            };

      foreach (var context in contexts)
      {
        logger.LogInformation("Applying migrations {migrations} for {context}", context.Database.GetPendingMigrations(), context);
        context.Database.Migrate();
      }
    }

    internal static IServiceProvider GetServiceProvider()
    {
      var host = new Microsoft.Extensions.Hosting.HostBuilder()
          .ConfigureHostConfiguration(configHost =>
          {
            configHost.SetBasePath(Directory.GetCurrentDirectory());
            configHost.AddJsonFile("appsettings.json", optional: true);
            configHost.AddEnvironmentVariables();
          })
          .ConfigureServices((hostContext, services) =>
          {
            services.AddFixtures();

            services.AddScoped<FixtureManager>();

            // apparemment quand on est en design time, dotnet ef rajoute tout seul un logger
            if (!EF.IsDesignTime)
            {
              services.AddLogging(opt => opt.AddSerilog(
                            new LoggerConfiguration()
                                .ReadFrom.Configuration(hostContext.Configuration)
                                .CreateLogger()
                        )
                    );
            }

            services.AddDbContext<CustomApplicationContext>(options =>
                  {
                options.UseNpgsql(hostContext.Configuration.GetConnectionString("PGSQL"),
                          opt => opt.MigrationsAssembly(typeof(Program).Assembly.FullName)
                      );
              });

          }).Build();

      var services = host.Services;

      services.GetRequiredService<ILogger<Program>>().LogInformation("Services configured");

      return services;
    }
  }
}
