using System;
using System.Threading.Tasks;

using Bones.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;

namespace XXXXX.Core.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var controllerName = string.Empty;

            try
            {
                var controllerActionDescriptor = context
                    .GetEndpoint()
                    .Metadata
                    .GetMetadata<ControllerActionDescriptor>();

                controllerName = controllerActionDescriptor.ControllerName;
            }
            catch
            {
                // Do nothing
            }

            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {
                _logger.LogError(unauthorizedException, "Access denied.");

                context.Response.Clear();
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access denied");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occured");

                context.Response.Clear();
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("It's an error");
            }

            return;
        }
    }
}