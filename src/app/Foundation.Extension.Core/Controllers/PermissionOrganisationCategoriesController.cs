using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/permission-organisation-categories")]
    public class PermissionOrganisationCategoriesController : ControllerBase
    {
        private readonly IPermissionOrganisationCategoryService _permissionOrganisationCategoryService;

        public PermissionOrganisationCategoriesController(IPermissionOrganisationCategoryService permissionOrganisationCategoryService)
        {
            _permissionOrganisationCategoryService = permissionOrganisationCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionOrganisationCategoryInfosViewModel>>> GetMany()
        {
            var result = await _permissionOrganisationCategoryService.GetMany();
            return Ok(result);
        }
    }
}