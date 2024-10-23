using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Context.DI;

namespace XXXXX.Context.Kernel.DI
{
	public static class DependencyInjector
	{
		public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddContextExtension<ApplicationContext>(configuration);

			services.AddRepositories();

			return services;
		}
	}
}