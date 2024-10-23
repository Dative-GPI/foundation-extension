using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Foundation.Extension.Proxy.Extensions;
using Foundation.Extension.Proxy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Foundation.Extension.Proxy.Controllers
{
	[Route("api/admin/v1")]
	public class AdminExtensionApplicationsController : ControllerBase
	{
		private ILogger<AdminExtensionApplicationsController> _logger;
		private IHttpClientFactory _httpClientFactory;
		private string _foundationPrefix;
		private string _localPrefix;
		private bool _enableInstalledExtensions;

		public AdminExtensionApplicationsController(
			ILogger<AdminExtensionApplicationsController> logger,
			IHttpClientFactory httpClientFactory,
			IConfiguration configuration)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
			_foundationPrefix = configuration.GetConnectionString("Foundation");
			_localPrefix = configuration.GetConnectionString("Local");
			_enableInstalledExtensions = configuration.GetValue<bool>("EnableInstalledExtensions", true);
		}

		[Route("extension-applications")]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateExtensionApplicationViewModel payload)
		{
			var foundationClient = _httpClientFactory.CreateClient();

			if (payload.ExtensionId.HasValue)
			{
				var foundationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix);

				var content = await foundationResponse.Content.ReadAsStringAsync();

				var result = JsonSerializer.Deserialize<JsonDocument>(content);

				return Ok(result);
			}

			var applicationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix, "/api/shared/v1/applications/current");
			applicationResponse.EnsureSuccessStatusCode();

			var applicationContent = await applicationResponse.Content.ReadAsStringAsync();
			var applicationDocument = JsonSerializer.Deserialize<JsonDocument>(applicationContent);
			var applicationId = applicationDocument.RootElement.GetProperty("id").GetGuid();

			var jwtResponse = await foundationClient.PostAsync(HttpContext, _foundationPrefix, "/api/shared/v1/pats", new
			{
				lifetime = 60 * 24 * 7, // one week
				label = "Foundation.Extension.Proxy - Admin JWT",
			});

			jwtResponse.EnsureSuccessStatusCode();

			var jwtContent = await jwtResponse.Content.ReadAsStringAsync();
			var jwtDocument = JsonSerializer.Deserialize<JsonDocument>(jwtContent);
			var jwt = jwtDocument.RootElement.GetProperty("token").GetString();

			_logger.LogInformation("Token acquired : {jwt}", jwt);

			var host = new Uri(_foundationPrefix).Host;

			var localClient = _httpClientFactory.CreateClient();
			var response = await localClient.SendAsync(new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				Content = JsonContent.Create(new
				{
					applicationId = applicationId,
					host = host,
					adminJWT = jwt
				}),
				RequestUri = new Uri($"{_localPrefix}/api/shared/v1/applications")
			});

			response.EnsureSuccessStatusCode();

			return Ok(new
			{
				id = Guid.NewGuid(),
				applicationId = applicationId,
				extensionId = (Guid?)null,
				label = "Local extension",
				description = "Added automatically by Foundation.Extension.Proxy",
			});
		}


		[Route("extension-applications")]
		[HttpGet]
		public async Task<IActionResult> GetMany()
		{
			var result = new List<JsonElement>();

			if (_enableInstalledExtensions)
			{
				var foundationClient = _httpClientFactory.CreateClient();
				var foundationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix);

				var content = await foundationResponse.Content.ReadAsStringAsync();
				var foundationResult = JsonSerializer.Deserialize<List<JsonElement>>(content);
				result.AddRange(foundationResult);
			}

			result.Add(JsonSerializer.SerializeToElement(new
			{
				description = "Added automatically by Foundation.Extension.Proxy",
				extensionId = (Guid?)null,
				id = (Guid?)null,
				label = "Local extension"
			}));

			return Ok(result);
		}
	}
}