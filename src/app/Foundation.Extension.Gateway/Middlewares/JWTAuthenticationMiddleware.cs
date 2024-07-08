using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.Gateway.Middlewares
{
  public class JWTAuthenticationMiddleware
  {
    public const string AuthenticationScheme = "CUSTOM";
    public const string JwtQueryParamKey = "access_token";
    private readonly RequestDelegate _next;
    private IServiceProvider _serviceProvider;
    private HttpClient _httpClient;
    private ILogger<JWTAuthenticationMiddleware> _logger;

    public JWTAuthenticationMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<JWTAuthenticationMiddleware> logger, HttpClient httpClient)
    {
      _next = next;
      _serviceProvider = serviceProvider;
      _logger = logger;
      _httpClient = httpClient;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      _logger.LogTrace("Invoked");

      if (context.GetEndpoint()?.Metadata.Any(m => m is AllowAnonymousAttribute) ?? false)
      {
        _logger.LogTrace("Anonymous request");
        await _next(context);
        return;
      }

      _logger.LogTrace("Authenticated request");

      using var scope = _serviceProvider.CreateScope();
      var sp = scope.ServiceProvider;

      string jwt = null;

      if (context.Request.Query.ContainsKey(JwtQueryParamKey))
      {
        jwt = context.Request.Query[JwtQueryParamKey].ToString();
        context.Request.Headers.Add(HeaderNames.Authorization, $"Bearer {jwt}");
      }

      if (context.Request.Headers.ContainsKey(HeaderNames.Authorization))
      {
        var bearer = context.Request.Headers[HeaderNames.Authorization].ToString();
        if (bearer.StartsWith("Bearer ")) jwt = bearer.Substring("Bearer ".Length);
      }
      else
      {
        _logger.LogError("No JWT token found in request");
        Unauthorized(context);
        return;
      }

      if (String.IsNullOrWhiteSpace(jwt))
      {
        _logger.LogError("JWT is empty");
        Unauthorized(context);
        return;
      }

      var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
      var claimsFactory = sp.GetRequiredService<IClaimsFactory>();

      var claims = claimsFactory.Get(token.Claims);

      var clientFactory = sp.GetRequiredService<IFoundationClientFactory>();

      var client = await clientFactory.CreateAuthenticated(claims.ApplicationId, claims.LanguageCode, jwt);

      var isAuthenticated = await client.Gateway.Accounts.IsAuthenticated();

      if (isAuthenticated)
      {
        _logger.LogTrace("Authentication succeed");

        ClaimsIdentity identity = new ClaimsIdentity(token.Claims, AuthenticationScheme);

        context.User = new ClaimsPrincipal(identity);
        await _next(context);
      }
      else
      {
        _logger.LogInformation("Unable to authenticate current request : User {user} - Source {source}", claims.UserId, claims.SourceId);
        Unauthorized(context);
        return;
      }
    }

    private void Unauthorized(HttpContext context)
    {
      context.Response.StatusCode = 401;
    }
  }
}