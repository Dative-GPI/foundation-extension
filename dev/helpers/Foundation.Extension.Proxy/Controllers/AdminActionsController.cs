using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Foundation.Extension.Proxy.Extensions;
using Foundation.Extension.Proxy.Tools;

namespace Foundation.Extension.Proxy.Controllers
{
    [Route("api/admin/v1")]
    public class AdminActionsController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private string _foundationPrefix;
        private LocalClient _localClient;
        private string _localPrefix;
        private bool _enableInstalledExtensions;

        public AdminActionsController(
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


        [HttpGet("actions")]
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

            var localResult = await _localClient.Get<List<JsonElement>>(HttpContext, "/api/admin/v1/actions");
            result.AddRange(localResult.Select(l => JsonSerializer.SerializeToElement(new
            {
                extensionId = (Guid?)null,
                actionType = l.GetProperty("actionType").GetInt32(),
                path = l.GetProperty("path").GetString(),
                label = l.GetProperty("label").GetString(),
                icon = l.GetProperty("icon").GetString(),
                uri = _localPrefix
            })));

            return Ok(result);
        }
    }
}