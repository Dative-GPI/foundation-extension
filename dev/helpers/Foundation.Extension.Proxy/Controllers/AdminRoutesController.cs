using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Foundation.Extension.Proxy.Extensions;
using System.Linq;
using Foundation.Extension.Proxy.Tools;
using Foundation.Clients.Admin.FoundationModels;

namespace Foundation.Extension.Proxy.Controllers
{
	[Route("api/admin/v1")]
	public class AdminRoutesController : ControllerBase
	{
		private IHttpClientFactory _httpClientFactory;
		private string _foundationPrefix;
		private LocalClient _localClient;
		private string _localPrefix;
		private bool _enableInstalledExtensions;

		public AdminRoutesController(
			IHttpClientFactory httpClientFactory,
			IConfiguration configuration,
			LocalClient localClient)
		{
			_httpClientFactory = httpClientFactory;
			_foundationPrefix = configuration.GetConnectionString("Foundation");
			_localPrefix = configuration.GetConnectionString("Local");
			_enableInstalledExtensions = configuration.GetValue<bool>("EnableInstalledExtensions", true);
			_localClient = localClient;
		}


		[HttpGet("routes")]
		public async Task<IActionResult> GetMany()
		{
			var result = new List<RouteInfosFoundationModel>();

			if (_enableInstalledExtensions)
			{
				var foundationClient = _httpClientFactory.CreateClient();
				var foundationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix);
				var foundationContent = await foundationResponse.Content.ReadAsStringAsync();
				var foundationResult = JsonSerializer.Deserialize<List<RouteInfosFoundationModel>>(foundationContent);
				result.AddRange(foundationResult);
			}

			var localResult = await _localClient.Get<List<JsonElement>>(HttpContext, "/api/admin/v1/routes");
			result.AddRange(localResult.Select(l => new RouteInfosFoundationModel()
			{
				DrawerCategory = l.GetProperty("drawerCategory").GetString(),
				DrawerIcon = l.GetProperty("drawerIcon").GetString(),
				DrawerLabel = l.GetProperty("drawerLabel").GetString(),
				Exact = l.GetProperty("exact").GetBoolean(),
				ExtensionId = (Guid?)null,
				Name = l.GetProperty("name").GetString(),
				Path = l.GetProperty("path").GetString(),
				ShowOnDrawer = l.GetProperty("showOnDrawer").GetBoolean(),
				Uri = _localPrefix
			}));

			return Ok(result);
		}
	}
}