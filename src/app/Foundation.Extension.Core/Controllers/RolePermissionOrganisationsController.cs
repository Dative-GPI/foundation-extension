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

        [HttpGet("service-account-role-organisation/{roleId:Guid}")]
        public async Task<IActionResult> GetServiceAccountRoleOrganisation([FromRoute] Guid roleId)
        {
            var result = await _rolePermissionOrganisationService.GetServiceAccountRoleOrganisation(roleId);
            return Ok(result);
        }

        [HttpGet("role-organisation-type/{roleId:Guid}")]
        public async Task<IActionResult> GetRoleOrganisationType([FromRoute] Guid roleId)
        {
            var result = await _rolePermissionOrganisationService.GetRoleOrganisationType(roleId);
            return Ok(result);
        }

        [HttpGet("role-organisation/{roleId:Guid}")]
        public async Task<IActionResult> GetRoleOrganisation([FromRoute] Guid roleId)
        {
            var result = await _rolePermissionOrganisationService.GetRoleOrganisation(roleId);
            return Ok(result);
        }

        [HttpPost("service-account-role-organisation/{roleId:Guid}")]
        public async Task<IActionResult> UpdateServiceAccountRoleOrganisation([FromRoute] Guid roleId, [FromBody] UpdateRolePermissionOrganisationViewModel payload)
        {
            var result = await _rolePermissionOrganisationService.UpdateServiceAccountRoleOrganisation(roleId, payload);
            return Ok(result);
        }

        [HttpPost("role-organisation/{roleId:Guid}")]
        public async Task<IActionResult> UpdateRoleOrganisation([FromRoute] Guid roleId, [FromBody] UpdateRolePermissionOrganisationViewModel payload)
        {
            var result = await _rolePermissionOrganisationService.UpdateRoleOrganisation(roleId, payload);
            return Ok(result);
        }
    }
}