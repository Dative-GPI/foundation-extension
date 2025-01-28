using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/permission-organisations")]
    public class PermissionOrganisationsController : ControllerBase
    {
        private readonly IPermissionOrganisationService _permissionOrganisationService;

        public PermissionOrganisationsController(IPermissionOrganisationService permissionOrganisationService)
        {
            _permissionOrganisationService = permissionOrganisationService;
        }

        [HttpGet("current")]
        public async Task<ActionResult<PermissionOrganisationInfosViewModel>> GetCurrent()
        {
            var result = await _permissionOrganisationService.GetCurrent();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PermissionOrganisationInfosViewModel>> GetMany()
        {
            var result = await _permissionOrganisationService.GetMany();
            return Ok(result);
        }
    }
}