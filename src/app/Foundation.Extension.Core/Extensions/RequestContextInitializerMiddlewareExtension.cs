using Microsoft.AspNetCore.Builder;

using Foundation.Extension.Core.Middlewares;

public static class RequestContextInitializerMiddlewareExtension
{
    public static IApplicationBuilder UseCoreExtension(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestContextInitializerMiddleware>();
    }
}