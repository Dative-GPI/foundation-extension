using System.Net.Http;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Foundation.Extension.Proxy.Tools;
using System.Net;
using Yarp.ReverseProxy.Forwarder;
using System.Diagnostics;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpForwarder();
builder.Services.AddHttpClient(string.Empty, c => { }).ConfigurePrimaryHttpMessageHandler(() =>
   new HttpClientHandler
   {
       ClientCertificateOptions = ClientCertificateOption.Manual,
       ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
   }
);
builder.Services.AddScoped<LocalClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders();

app.UseRouting();

app.MapControllers();

var httpClient = new HttpMessageInvoker(new SocketsHttpHandler()
{
    UseProxy = false,
    AllowAutoRedirect = false,
    AutomaticDecompression = DecompressionMethods.None,
    UseCookies = false,
    ActivityHeadersPropagator = new ReverseProxyPropagator(DistributedContextPropagator.Current),
    ConnectTimeout = TimeSpan.FromSeconds(15),
    SslOptions = new System.Net.Security.SslClientAuthenticationOptions()
    {
        RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
    }
});

app.MapForwarder("/{**catch-all}", app.Configuration.GetConnectionString("Foundation"), ForwarderRequestConfig.Empty, HttpTransformer.Default, httpClient);

app.Run();
