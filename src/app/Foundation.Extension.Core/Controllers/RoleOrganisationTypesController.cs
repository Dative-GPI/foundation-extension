using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

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
        public async Task<ActionResult<RoleOrganisationTypeDetailsViewModel>> Get([FromRoute] Guid roleOrganisationTypeId)
        {
            var result = await _roleOrganisationTypeService.Get(roleOrganisationTypeId);
            return Ok(result);
        }
    }
}