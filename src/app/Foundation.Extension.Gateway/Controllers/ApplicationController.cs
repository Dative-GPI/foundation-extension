using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Controllers
{
    [Route("api")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [Route("applications")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Install([FromBody]CreateApplicationViewModel payload)
        {
            var result = await _applicationService.Create(payload);

            return Ok(result);
        }

        [Route("applications/{applicationId:Guid}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> Uninstall([FromRoute]Guid applicationId)
        {
            await _applicationService.Remove(applicationId);
            return Ok();
        }
    }
}