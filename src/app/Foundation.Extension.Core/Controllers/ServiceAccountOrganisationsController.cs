using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/service-account-organisations")]
    public class ServiceAccountOrganisationsController : ControllerBase
    {
        private readonly IServiceAccountOrganisationService _serviceAccountOrganisationService;

        public ServiceAccountOrganisationsController(IServiceAccountOrganisationService serviceAccountOrganisationService)
        {
            _serviceAccountOrganisationService = serviceAccountOrganisationService;
        }

        [HttpGet("{serviceAccountOrganisationId:Guid}")]
        public async Task<ActionResult<ServiceAccountOrganisationDetailsViewModel>> Get([FromRoute] Guid serviceAccountOrganisationId)
        {
            var result = await _serviceAccountOrganisationService.Get(serviceAccountOrganisationId);
            return Ok(result);
        }
    }
}