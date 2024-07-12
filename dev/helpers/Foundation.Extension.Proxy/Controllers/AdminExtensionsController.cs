using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Foundation.Extension.Proxy.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Foundation.Extension.Proxy.Controllers
{
    [Route("api/admin/v1")]
    public class AdminExtensionsController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private string _foundationPrefix;
        private bool _enableInstalledExtensions;
        private string _localPrefix;

        public AdminExtensionsController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _foundationPrefix = configuration.GetConnectionString("Foundation");
            _localPrefix = configuration.GetConnectionString("Local");
            _enableInstalledExtensions = configuration.GetValue<bool>("EnableInstalledExtensions", true);
        }


        [HttpGet("extensions")]
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
                id = (Guid?)null,
                @public = false,
                label = "Local extension",
                description = "Added automatically by Foundation.Extension.Proxy",
                translations = new List<JsonElement>()
            }));

            return Ok(result);
        }


        [HttpGet("extensions/null")]
        public IActionResult GetRoleExtension()
        {
            return Ok(new
            {
                id = (Guid?)null,
                extensionId = (Guid?)null,
                shellHost = new Uri(_localPrefix).Host,
                adminHost = new Uri(_localPrefix).Host,
                host = new Uri(_localPrefix).Host,
                label = "Local extension",
                description = "Added automatically by Foundation.Extension.Proxy"
            });
        }
    }
}