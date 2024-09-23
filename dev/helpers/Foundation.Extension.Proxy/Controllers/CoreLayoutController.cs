
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;
using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Proxy.Extensions;
using Foundation.Extension.Proxy.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Foundation.Extension.Proxy.Controllers
{
    [Route("api/foundation/core/v1/organisations/{organisationId}")]
    public class CoreLayoutController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private string _foundationPrefix;
        private string _localPrefix;
        private bool _enableInstalledExtensions;
        private LocalClient _localClient;

        public CoreLayoutController(
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

        [HttpGet("layouts/current")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid organisationId)
        {
            var localRoutes = await _localClient.Get<List<RouteInfosFoundationModel>>(HttpContext, "/api/core/v1/organisations/" + organisationId + "/routes");

            var foundationClient = _httpClientFactory.CreateClient();
            var foundationResponse = await foundationClient.GetAsync(HttpContext, _foundationPrefix);
            var foundationResult = await foundationResponse.Content.ReadFromJsonAsync<LayoutDetailsFoundationModel>();

            if (!_enableInstalledExtensions)
            {
                foundationResult.Pages.RemoveAll( p => foundationResult.ExternalRoutes.Any(r => r.Name == p.PageCode));
                foundationResult.Categories.RemoveAll(p => !foundationResult.Pages.Any(r => r.LayoutCategoryId == p.Id));
                foundationResult.ExternalRoutes.Clear();
            }

            foundationResult.Categories.Add(new LayoutCategoryInfosFoundationModel()
            {
                Id = Guid.Empty,
                Index = 99,
                Icon = "mdi-home-account",
                Label = "Extensions Category"
            });

            foundationResult.Pages.AddRange(
                localRoutes
                .Where(l => l.ShowOnDrawer)
                .Select(l => new LayoutPageInfosFoundationModel
                {
                    Id = Guid.NewGuid(),
                    LayoutCategoryId = Guid.Empty,
                    Index = 0,
                    Icon = "mdi-home-account",
                    Label = l.DrawerLabel,
                    PageCode = l.Name,
                    PageId = Guid.NewGuid(),
                    PageType = PageType.Extension
                })
                .ToList()
            );

            foundationResult.ExternalRoutes.AddRange(
                localRoutes
                .Select(l => new RouteInfosFoundationModel
                {
                    DrawerCategory = "Extensions Category",
                    Id = Guid.NewGuid(),
                    Name = l.Name,
                    Path = l.Path,
                    Uri = _localPrefix,
                    ExtensionId = null
                })
                .ToList()
            );

            return Ok(foundationResult);
        }
    }
}