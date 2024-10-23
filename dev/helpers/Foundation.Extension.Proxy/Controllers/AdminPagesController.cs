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

namespace Foundation.Extension.Proxy.Controllers
{
	[Route("api/admin/v1")]
	public class AdminPagesController : ControllerBase
	{
		private IHttpClientFactory _httpClientFactory;
		private string _foundationPrefix;
		private LocalClient _localClient;
		private string _localPrefix;
		private bool _enableInstalledExtensions;

		public AdminPagesController(
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


		[HttpGet("pages")]
		public async Task<IActionResult> GetMany()
		{
			var result = new List<JsonElement>();

			if (_enableInstalledExtensions)
			{
				var foundationClient = _httpClientFactory.CreateClient();
				var foundationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix);
				var foundationContent = await foundationResponse.Content.ReadAsStringAsync();
				var foundationResult = JsonSerializer.Deserialize<List<JsonElement>>(foundationContent);
				result.AddRange(foundationResult);
			}

			var localResult = await _localClient.Get<List<JsonElement>>(HttpContext, "/api/admin/v1/pages");
			result.AddRange(localResult.Select(l => JsonSerializer.SerializeToElement(new
			{
				Id = (Guid?)null,
				Type = 2,
				Code = l.GetProperty("code").GetString(),
				LabelDefault = l.GetProperty("labelDefault").GetString()
			})));

			return Ok(result);
		}
	}
}