using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/role-organisation-types")]
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
    }
}