using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/user-permission-organisations")]
    public class UserPermissionOrganisationsController : ControllerBase
    {
        private readonly IUserPermissionOrganisationService _userPermissionOrganisationService;

        public UserPermissionOrganisationsController(IUserPermissionOrganisationService userPermissionOrganisationService)
        {
            _userPermissionOrganisationService = userPermissionOrganisationService;
        }

        [HttpGet("{userId:Guid}/{userType:int}")]
        public async Task<IActionResult> GetUserPermissionOrganisation([FromRoute] Guid userId, [FromRoute] UserType userType)
        {
            var result = await _userPermissionOrganisationService.GetUserPermissionOrganisation(userId, userType);
            return Ok(result);
        }
    }
}