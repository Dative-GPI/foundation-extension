using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/permission-organisation-categories")]
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