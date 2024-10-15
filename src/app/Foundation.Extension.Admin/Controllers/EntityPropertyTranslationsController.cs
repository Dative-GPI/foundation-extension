using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;

namespace Foundation.Extension.Admin.Controllers
{
    [Route("api/admin/v1")]
    public class EntityPropertyTranslationsController : ControllerBase
    {
        private readonly IEntityPropertyTranslationService _entityPropertyTranslationService;

        public EntityPropertyTranslationsController(IEntityPropertyTranslationService entityPropertyTranslationService)
        {
            _entityPropertyTranslationService = entityPropertyTranslationService;
        }

        [HttpGet("entity-property-translations")]
        public async Task<ActionResult<IEnumerable<EntityPropertyTranslationViewModel>>> GetMany([FromQuery] EntityPropertyTranslationsFilterViewModel filter)
        {
            var result = await _entityPropertyTranslationService.GetMany(filter);
            return Ok(result);
        }

        [HttpPut("entity-property-translations/{entityPropertyId}")]
        public async Task<ActionResult<IEnumerable<EntityPropertyTranslationViewModel>>> Replace([FromRoute] Guid entityPropertyId, [FromBody] List<UpdateEntityPropertyTranslationViewModel> payload)
        {
            var result = await _entityPropertyTranslationService.Replace(entityPropertyId, payload);
            return Ok(result);
        }

        [HttpGet("entity-property-translations/workbook")]
        public async Task<FileContentResult> Download([FromQuery] string fileName)
        {
            var data = await _entityPropertyTranslationService.Download();
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPut("entity-property-translations/workbook")]
        public async Task<ActionResult<IEnumerable<EntityPropertyTranslationViewModel>>> Upload([FromForm] List<SpreadsheetColumnDefinitionViewModel> languages, [FromForm] IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var result = await _entityPropertyTranslationService.Upload(languages, stream);
                return Ok(result);
            }
        }
    }
}
