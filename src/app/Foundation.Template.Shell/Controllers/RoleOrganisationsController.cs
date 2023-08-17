using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Template.Shell.Abstractions;
using Foundation.Template.Shell.ViewModels;

namespace Foundation.Template.Shell.Controllers
{
    [Route("api/v1")]
    public class RoleOrganisationsController : ControllerBase
    {
        private readonly IRoleOrganisationService _roleOrganisationService;

        public RoleOrganisationsController(IRoleOrganisationService roleOrganisationService)
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
        public async Task<IActionResult> Update([FromRoute] Guid organisationId, [FromRoute] Guid roleId, [FromBody] UpdateRoleOrganisationViewModel payload)
        {
            await _roleOrganisationService.Update(roleId, payload);
            return Ok();
        }
    }
}