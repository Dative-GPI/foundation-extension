using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/role-organisations")]
    public class RoleOrganisationsController : ControllerBase
    {
        private readonly IRoleOrganisationService _roleOrganisationService;

        public RoleOrganisationsController(IRoleOrganisationService roleOrganisationService)
        {
            _roleOrganisationService = roleOrganisationService;
        }

        [HttpGet("{roleOrganisationId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid roleOrganisationId)
        {
            var result = await _roleOrganisationService.Get(roleOrganisationId);
            return Ok(result);
        }

        [HttpPost("{roleOrganisationId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid roleOrganisationId, [FromBody] UpdateRoleOrganisationViewModel payload)
        {
            var result = await _roleOrganisationService.Update(roleOrganisationId, payload);
            return Ok(result);
        }
    }
}