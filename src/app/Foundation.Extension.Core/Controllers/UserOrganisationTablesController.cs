using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.API.Controllers
{
  [Route("api/core/v1")]
  public class UserOrganisationTablesController : ControllerBase
  {
    private readonly IUserOrganisationTableService _tableService;
    public UserOrganisationTablesController(IUserOrganisationTableService tableService)
    {
      _tableService = tableService;
    }

    [HttpGet("user-organisation-tables/{tableCode}")]
    public async Task<ActionResult<UserOrganisationTableDetailsViewModel>> GetMany([FromRoute] string tableCode)
    {
      var result = await _tableService.GetMany(tableCode);
      return Ok(result);
    }

    [HttpPost("user-organisation-tables/{tableCode}")]
    public async Task<OkResult> Update([FromRoute] string tableCode, [FromBody] UpdateUserOrganisationTableViewModel payload)
    {
      await _tableService.Update(tableCode, payload);
      return Ok();
    }
  }
}
