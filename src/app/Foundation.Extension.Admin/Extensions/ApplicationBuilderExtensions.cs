using Microsoft.AspNetCore.Builder;

using Foundation.Extension.Admin.Middlewares;

namespace Foundation.Extension.Admin.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAdminExtension(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestContextInitializerMiddleware>();
        }
    }
}