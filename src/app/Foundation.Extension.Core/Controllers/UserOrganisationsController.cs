using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/user-organisations")]
    public class UserOrganisationsController : ControllerBase
    {
        private readonly IUserOrganisationService _userOrganisationService;

        public UserOrganisationsController(IUserOrganisationService userOrganisationService)
        {
            _userOrganisationService = userOrganisationService;
        }

        [HttpGet("current")]
        public async Task<ActionResult<UserOrganisationDetailsViewModel>> GetCurrent()
        {
            var result = await _userOrganisationService.GetCurrent();
            return Ok(result);
        }

        [HttpGet("{userOrganisationId:Guid}")]
        public async Task<ActionResult<UserOrganisationDetailsViewModel>> Get([FromRoute] Guid userOrganisationId)
        {
            var result = await _userOrganisationService.Get(userOrganisationId);
            return Ok(result);
        }
    }
}