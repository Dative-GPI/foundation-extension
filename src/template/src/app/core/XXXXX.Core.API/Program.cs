using System.Net;
using System.Net.Http;
using System.Diagnostics;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Foundation.Extension.Core.DI;
using Foundation.Extension.CrossCutting.DI;

using Foundation.Extension.Core.Extensions;

using XXXXX.Core.Kernel.DI;
using XXXXX.Context.Kernel.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddKernel(builder.Configuration);
builder.Services.AddCoreExtension(builder.Configuration);
builder.Services.AddContext(builder.Configuration);
builder.Services.AddCrossCutting(builder.Configuration);


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
  app.UseDeveloperExceptionPage();
}

app.UseCoreExtension();

app.MapControllers();
app.MapCoreExtensionControllers();

app.Run();
