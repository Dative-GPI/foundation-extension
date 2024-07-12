using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using XXXXX.Admin.Kernel.Services;
using Foundation.Extension.Admin.Abstractions;

namespace XXXXX.Admin.Kernel.DI
{
  public static class DependencyInjector
  {
    public static IServiceCollection AddKernel(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddFlow();

      services.AddScoped<IRoutesProvider, RoutesProvider>();
      services.AddScoped<IActionsProvider, ActionsProvider>();

      services.AddAutoMapper(typeof(DependencyInjector).Assembly);

      return services;
    }
  }
}