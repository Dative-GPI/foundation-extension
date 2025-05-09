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
using Foundation.Clients.Core.FoundationModels;

namespace Foundation.Extension.Proxy.Controllers
{
    [Route("api/foundation/core/v1/organisations/{organisationId:Guid}")]
    public class CoreWidgetTemplatesController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private string _foundationPrefix;
        private string _localPrefix;
        private LocalClient _localClient;

        public CoreWidgetTemplatesController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            LocalClient localClient)
        {
            _httpClientFactory = httpClientFactory;
            _foundationPrefix = configuration.GetConnectionString("Foundation");
            _localPrefix = configuration.GetConnectionString("Local");
            _localClient = localClient;
        }


        [HttpGet("widget-templates")]
        public async Task<IActionResult> GetMany()
        {
            var result = new List<JsonElement>();

            var organisationId = Guid.Parse(HttpContext.Request.RouteValues["organisationId"].ToString());

            var foundationClient = _httpClientFactory.CreateClient();
            var foundationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix);
            var foundationContent = await foundationResponse.Content.ReadAsStringAsync();
            var foundationResult = JsonSerializer.Deserialize<List<JsonElement>>(foundationContent);
            result.AddRange(foundationResult);


            var localResult = await _localClient.Get<List<JsonElement>>(HttpContext, "/api/core/v1/organisations/" + organisationId + "/widget-templates");
            var localUrl = new Uri(_localPrefix);
            var localHost = localUrl.Host;

            result.AddRange(localResult.Select(l => JsonSerializer.SerializeToElement(new
            {
                id = l.GetProperty("id"),
                type = WidgetTemplateType.Extension,
                code = l.GetProperty("code"),
                icon = l.GetProperty("icon"),
                category = l.GetProperty("category"),
                defaultWidth = l.GetProperty("defaultWidth"),
                defaultHeight = l.GetProperty("defaultHeight"),
                defaultMeta = l.GetProperty("defaultMeta"),
                extensionId = (Guid?)null,
                extensionHost = localHost,
                label = l.GetProperty("label"),
                description = l.GetProperty("description"),
            })));

            return Ok(result);
        }
    }
}