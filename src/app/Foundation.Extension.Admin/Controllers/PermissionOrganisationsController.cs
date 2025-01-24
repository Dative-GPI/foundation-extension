using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/permission-organisations")]
    public class PermissionOrganisationsController : ControllerBase
    {
        private readonly IPermissionOrganisationService _permissionOrganisationService;

        public PermissionOrganisationsController(IPermissionOrganisationService permissionOrganisationService)
        {
            _permissionOrganisationService = permissionOrganisationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMany([FromQuery] PermissionOrganisationsFilterViewModel filter)
        {
            var result = await _permissionOrganisationService.GetMany(filter);
            return Ok(result);
        }
    }
}