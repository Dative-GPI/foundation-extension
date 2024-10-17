using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1")]
    public class ApplicationTranslationsController : ControllerBase
    {
        private readonly IApplicationTranslationService _applicationTranslationService;

        public ApplicationTranslationsController(IApplicationTranslationService applicationTranslationService)
        {
            _applicationTranslationService = applicationTranslationService;
        }

        [HttpGet("application-translations")]
        public async Task<IActionResult> GetMany([FromQuery] ApplicationTranslationViewModel filter)
        {
            var translations = await _applicationTranslationService.GetMany(filter);
            return Ok(translations);
        }

        [HttpPost("application-translations/{code}")]
        public async Task<IActionResult> Update([FromRoute] string code, [FromBody] UpdateApplicationTranslationViewModel payload)
        {
            var applicationTranslations = await _applicationTranslationService.Update(code, payload);
            return Ok(applicationTranslations);
        }

        [HttpGet("application-translations/workbook")]
        public async Task<FileContentResult> Download([FromQuery] string fileName)
        {
            var data = await _applicationTranslationService.Download();
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPut("application-translations/workbook")]
        public async Task<ActionResult<IEnumerable<ApplicationTranslationViewModel>>> Upload([FromForm] IEnumerable<SpreadsheetColumnDefinitionViewModel> languages, [FromForm] IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _applicationTranslationService.Upload(languages, stream);
                return Ok(result);
            }
        }
    }
}
