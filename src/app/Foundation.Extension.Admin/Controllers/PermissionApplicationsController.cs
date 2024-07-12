using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1")]
    public class PermissionApplicationsController : ControllerBase
    {
        private readonly IPermissionApplicationService _permissionApplicationService;

        public PermissionApplicationsController(IPermissionApplicationService permissionApplicationService)
        {
            _permissionApplicationService = permissionApplicationService;
        }

        [Route("permission-applications")]
        [HttpGet]
        public async Task<IActionResult> GetMany([FromQuery] PermissionApplicationFilterViewModel filter)
        {
            var result = await _permissionApplicationService.GetMany(filter);
            return Ok(result);
        }

        [Route("permission-applications/current")]
        [HttpGet]
        public async Task<IActionResult> GetCurrent()
        {
            var result = await _permissionApplicationService.GetCurrent();
            return Ok(result);
        }
    }
}