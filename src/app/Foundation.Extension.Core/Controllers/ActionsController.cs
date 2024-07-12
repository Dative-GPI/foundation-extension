using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1")]
    public class ActionsController : ControllerBase
    {
        private IActionService _actionService;

        public ActionsController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpGet("organisations/{organisationId:Guid}/actions")]
        public async Task<IActionResult> GetMany(Guid organisationId, [FromQuery] ActionsFilterViewModel filter)
        {
            var actions = await _actionService.GetMany(filter);
            return Ok(actions);
        }
    }
}