using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Extension.Core.Extensions
{
    public static class StartupExtensions
    {
        public static IEndpointRouteBuilder MapCoreExtensionControllers(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllers();

            return endpoints;
        }
    }
}
