using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/permission-organisation-types")]
    public class PermissionOrganisationTypesController : ControllerBase
    {
        private readonly IPermissionOrganisationTypeService _permissionOrganisationTypeService;

        public PermissionOrganisationTypesController(IPermissionOrganisationTypeService permissionOrganisationTypeService)
        {
            _permissionOrganisationTypeService = permissionOrganisationTypeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetMany([FromQuery] PermissionOrganisationTypesFilterViewModel filter)
        {
            var result = await _permissionOrganisationTypeService.GetMany(filter);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<IEnumerable<PermissionOrganisationTypeInfosViewModel>>> Upsert([FromBody] List<UpsertPermissionOrganisationTypesViewModel> payload)
        {
            var result = await _permissionOrganisationTypeService.Upsert(payload);
            return Ok(result);
        }
    }
}