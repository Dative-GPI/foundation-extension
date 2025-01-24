using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1/permission-application-categories")]
    public class PermissionApplicationCategoriesController : ControllerBase
    {
        private readonly IPermissionApplicationCategoryService _permissionApplicationCategoryService;

        public PermissionApplicationCategoriesController(IPermissionApplicationCategoryService permissionApplicationCategoryService)
        {
            _permissionApplicationCategoryService = permissionApplicationCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMany()
        {
            var result = await _permissionApplicationCategoryService.GetMany();
            return Ok(result);
        }
    }
}