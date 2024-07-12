using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/routes")]
    public class RoutesController : ControllerBase
    {
        private IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMany()
        {
            var routes = await _routeService.GetMany();
            return Ok(routes);
        }
    }
}
