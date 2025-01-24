using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/permissions")]
    public class PermissionOrganisationsController : ControllerBase
    {
        private readonly IPermissionOrganisationService _permissionOrganisationService;

        public PermissionOrganisationsController(IPermissionOrganisationService permissionOrganisationService)
        {
            _permissionOrganisationService = permissionOrganisationService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _permissionOrganisationService.GetCategories();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMany()
        {
            var result = await _permissionOrganisationService.GetMany();
            return Ok(result);
        }
    }
}