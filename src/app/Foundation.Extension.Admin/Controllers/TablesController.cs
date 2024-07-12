using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.API.Controllers
{
    [Route("api/admin/v1")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet("tables/{tableId:Guid}")]
        public async Task<ActionResult<ApplicationTableDetailsViewModel>> Get([FromRoute] Guid tableId)
        {
            var result = await _tableService.Get(tableId);
            return Ok(result);
        }

        [HttpPost("tables/{tableId:Guid}")]
        public async Task<OkResult> Update([FromRoute] Guid tableId, [FromBody] UpdateTableViewModel payload)
        {
            await _tableService.Update(tableId, payload);
            return Ok();
        }

        [HttpPatch("tables/{tableId:Guid}/properties")]
        public async Task<ActionResult<ApplicationTableDetailsViewModel>> Patch([FromRoute] Guid tableId)
        {
            var result = await _tableService.Patch(tableId);
            return Ok(result);
        }

        [HttpGet("tables")]
        public async Task<ActionResult<IEnumerable<ApplicationTableInfosViewModel>>> GetMany([FromQuery] TableFiltersViewModel filter)
        {
            var result = await _tableService.GetMany(filter);
            return Ok(result);
        }
    }
}
