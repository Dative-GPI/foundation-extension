using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1")]
    public class RolePermissionOrganisationsController : ControllerBase
    {
        private readonly IRolePermissionOrganisationService _roleOrganisationService;

        public RolePermissionOrganisationsController(IRolePermissionOrganisationService roleOrganisationService)
        {
            _roleOrganisationService = roleOrganisationService;
        }

        [Route("organisations/{organisationId:Guid}/roles/{roleId:Guid}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid roleId)
        {
            var result = await _roleOrganisationService.Get(roleId);
            return Ok(result);
        }

        [Route("organisations/{organisationId:Guid}/roles/{roleId:Guid}")]
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] Guid organisationId, [FromRoute] Guid roleId, [FromBody] UpdateRolePermissionOrganisationViewModel payload)
        {
            await _roleOrganisationService.Update(roleId, payload);
            return Ok();
        }
    }
}
