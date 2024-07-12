using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1")]
    public class RoleApplicationsController : ControllerBase
    {
        private readonly IRoleApplicationService _roleApplicationService;

        public RoleApplicationsController(IRoleApplicationService roleApplicationService)
        {
            _roleApplicationService = roleApplicationService;
        }
        
        [Route("role-applications/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetMany([FromRoute] Guid id)
        {
            var result = await _roleApplicationService.Get(id);
            return Ok(result);
        }

        [Route("role-applications/{id:Guid}")]
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRoleApplicationViewModel payload)
        {
            var result = await _roleApplicationService.Update(id, payload);
            return Ok(result);
        }
    }
}