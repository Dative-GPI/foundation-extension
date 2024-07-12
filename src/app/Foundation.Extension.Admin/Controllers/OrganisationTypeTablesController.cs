using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1")]
    public class OrganisationTypeTablesController : ControllerBase
    {
        private readonly IOrganisationTypeTableService _organisationTypeDispositionService;

        public OrganisationTypeTablesController(IOrganisationTypeTableService columnOrganisationTypeService)
        {
            _organisationTypeDispositionService = columnOrganisationTypeService;
        }

        [HttpGet("organisation-types/{organisationTypeId:Guid}/tables/{tableId}")]
        public async Task<ActionResult<OrganisationTypeTableDetailsViewModel>> Get([FromRoute] Guid organisationTypeId, [FromRoute] Guid tableId)
        {
            var result = await _organisationTypeDispositionService.Get(organisationTypeId, tableId);
            return Ok(result);
        }

        [HttpPost("organisation-types/{organisationTypeId:Guid}/tables/{tableId}")]
        public async Task<ActionResult<OrganisationTypeTableDetailsViewModel>> Update([FromRoute] Guid organisationTypeId, [FromRoute] Guid tableId, [FromBody] UpdateOrganisationTypeTableViewModel payload)
        {
            var result = await _organisationTypeDispositionService.Update(organisationTypeId, tableId, payload);
            return Ok(result);
        }
    }
}