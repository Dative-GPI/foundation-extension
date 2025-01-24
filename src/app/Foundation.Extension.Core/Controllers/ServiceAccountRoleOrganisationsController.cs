using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/service-account-role-organisations")]
    public class ServiceAccountRoleOrganisationsController : ControllerBase
    {
        private readonly IServiceAccountRoleOrganisationService _serviceAccountRoleOrganisationService;

        public ServiceAccountRoleOrganisationsController(IServiceAccountRoleOrganisationService serviceAccountRoleOrganisationService)
        {
            _serviceAccountRoleOrganisationService = serviceAccountRoleOrganisationService;
        }

        [HttpGet("{serviceAccountRoleOrganisationId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid serviceAccountRoleOrganisationId)
        {
            var result = await _serviceAccountRoleOrganisationService.Get(serviceAccountRoleOrganisationId);
            return Ok(result);
        }

        [HttpPost("{serviceAccountRoleOrganisationId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid serviceAccountRoleOrganisationId, [FromBody] UpdateServiceAccountRoleOrganisationViewModel payload)
        {
            var result = await _serviceAccountRoleOrganisationService.Update(serviceAccountRoleOrganisationId, payload);
            return Ok(result);
        }
    }
}