using System.Net;
using System.Net.Http;
using System.Diagnostics;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Gateway.DI;
using Foundation.Extension.CrossCutting.DI;
using Foundation.Extension.Gateway.Extensions;

using XXXXX.Gateway.Kernel.DI;
using XXXXX.Context.Kernel.DI;

using Foundation.Extension.Gateway.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddKernel(builder.Configuration);
builder.Services.AddGatewayExtension(builder.Configuration);
builder.Services.AddContext(builder.Configuration);
builder.Services.AddCrossCutting(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Services.AddHttpClient(string.Empty, c => { }).ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
      ClientCertificateOptions = ClientCertificateOption.Manual,
      ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  // app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
  ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

app.UseHealthChecks("/health");

app.UseRouting();

app.UseExtensionAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapGatewayExtensionEndpoints();

  endpoints.MapForwarder("/api/admin/{**catch-all}", builder.Configuration.GetConnectionString("Admin"))
      .RequireAuthorization();

  endpoints.MapForwarder("/api/core/{**catch-all}", builder.Configuration.GetConnectionString("Core"))
      .RequireAuthorization();
});

app.Run();
