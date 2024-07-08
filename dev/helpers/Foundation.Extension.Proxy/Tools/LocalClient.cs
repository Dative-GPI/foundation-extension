using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Foundation.Extension.Proxy.Extensions;

namespace Foundation.Extension.Proxy.Tools
{
  public class LocalClient
  {
    private HttpClient _localClient;
    private ILogger<LocalClient> _logger;
    private string _foundationPrefix;
    private HttpClient _foundationClient;

    public LocalClient(
        ILogger<LocalClient> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
      _logger = logger;

      var localPrefix = configuration.GetConnectionString("Local");
      _foundationPrefix = configuration.GetConnectionString("Foundation");

      _localClient = httpClientFactory.CreateClient();
      _localClient.BaseAddress = new Uri(localPrefix);

      _foundationClient = httpClientFactory.CreateClient();
    }

    public async Task<TResult> Get<TResult>(HttpContext context, string path)
    {
      await PrepareClient(context);

      var uri = path + context.Request.QueryString.ToString();

      _logger.LogInformation("GET {uri}", uri);

      var response = await _localClient.GetAsync(uri);

      response.EnsureSuccessStatusCode();

      var content = await response.Content.ReadAsStringAsync();

      return JsonSerializer.Deserialize<TResult>(content, new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      });
    }


    public async Task<TResult> Post<TRequest, TResult>(HttpContext context, string path, TRequest payload)
    {
      await PrepareClient(context);

      var uri = path + context.Request.QueryString.ToString();

      _logger.LogInformation("POST {uri}", uri);

      var response = await _localClient.PostAsync(uri, JsonContent.Create(payload));

      response.EnsureSuccessStatusCode();

      var content = await response.Content.ReadAsStringAsync();

      return JsonSerializer.Deserialize<TResult>(content);
    }

    public async Task PrepareClient(HttpContext context)
    {
      var jwtResponse = await _foundationClient.PostAsync(context, _foundationPrefix, "/api/shared/v1/pats", new
      {
        lifetime = 60, // one hour
        label = "Foundation.Extension.Proxy - LocalClient JWT",
      });

      jwtResponse.EnsureSuccessStatusCode();

      var jwtContent = await jwtResponse.Content.ReadAsStringAsync();
      var jwtDocument = JsonSerializer.Deserialize<JsonDocument>(jwtContent);
      var jwt = jwtDocument.RootElement.GetProperty("token").GetString();

      _logger.LogInformation("Token acquired : {jwt}", jwt);

      _localClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);
    }
  }
}