using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/role-organisation-types")]
    public class RoleOrganisationTypesController : ControllerBase
    {
        private readonly IRoleOrganisationTypeService _roleOrganisationTypeService;

        public RoleOrganisationTypesController(IRoleOrganisationTypeService roleOrganisationTypeService)
        {
            _roleOrganisationTypeService = roleOrganisationTypeService;
        }
        
        [HttpGet("{roleOrganisationTypeId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid roleOrganisationTypeId)
        {
            var result = await _roleOrganisationTypeService.Get(roleOrganisationTypeId);
            return Ok(result);
        }

        [HttpPost("{roleOrganisationTypeId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid roleOrganisationTypeId, [FromBody] UpdateRoleOrganisationTypeViewModel payload)
        {
            var result = await _roleOrganisationTypeService.Update(roleOrganisationTypeId, payload);
            return Ok(result);
        }
    }
}