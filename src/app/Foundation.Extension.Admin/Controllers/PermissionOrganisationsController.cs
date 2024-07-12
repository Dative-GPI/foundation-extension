using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1")]
    public class PermissionOrganisationsController : ControllerBase
    {
        private readonly IPermissionOrganisationService _permissionService;

        public PermissionOrganisationsController(IPermissionOrganisationService permissionService)
        {
            _permissionService = permissionService;
        }

        [Route("permission-organisations")]
        [HttpGet]
        public async Task<IActionResult> GetMany([FromQuery] PermissionsFilterViewModel filter)
        {
            var result = await _permissionService.GetMany(filter);
            return Ok(result);
        }
    }
}