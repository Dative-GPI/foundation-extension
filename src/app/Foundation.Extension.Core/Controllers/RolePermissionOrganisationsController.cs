using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/role-permission-organisations")]
    public class RolePermissionOrganisationsController : ControllerBase
    {
        private readonly IRolePermissionOrganisationService _rolePermissionOrganisationService;

        public RolePermissionOrganisationsController(IRolePermissionOrganisationService rolePermissionOrganisationService)
        {
            _rolePermissionOrganisationService = rolePermissionOrganisationService;
        }

        [HttpGet("{roleId:Guid}")]
        public async Task<IActionResult> GetRolePermissionOrganisation([FromRoute] Guid roleId)
        {
            var result = await _rolePermissionOrganisationService.GetRolePermissionOrganisation(roleId);
            return Ok(result);
        }

        [HttpPost("{roleId:Guid}")]
        public async Task<IActionResult> UpdateRolePermissionOrganisation([FromRoute] Guid roleId, [FromBody] UpdateRolePermissionOrganisationViewModel payload)
        {
            var result = await _rolePermissionOrganisationService.UpdateRolePermissionOrganisation(roleId, payload);
            return Ok(result);
        }
    }
}