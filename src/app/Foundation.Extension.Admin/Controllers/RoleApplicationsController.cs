using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/role-applications")]
    public class RoleApplicationsController : ControllerBase
    {
        private readonly IRoleApplicationService _roleApplicationService;

        public RoleApplicationsController(IRoleApplicationService roleApplicationService)
        {
            _roleApplicationService = roleApplicationService;
        }
        
        [HttpGet("{roleApplicationId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid roleApplicationId)
        {
            var result = await _roleApplicationService.Get(roleApplicationId);
            return Ok(result);
        }

        [HttpPost("{roleApplicationId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid roleApplicationId, [FromBody] UpdateRoleApplicationViewModel payload)
        {
            var result = await _roleApplicationService.Update(roleApplicationId, payload);
            return Ok(result);
        }
    }
}