using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.ViewModels;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Controllers
{
  [Route("api/core/v1")]
  public class RoutesController : ControllerBase
  {
    private IRouteService _routeService;

    public RoutesController(IRouteService routeService)
    {
      _routeService = routeService;
    }

    [HttpGet("organisations/{organisationId:Guid}/routes")]
    public async Task<IActionResult> GetMany()
    {
      var routes = await _routeService.GetMany();
      return Ok(routes);
    }
  }
}
