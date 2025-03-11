using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1")]
    public class WidgetTemplatesController : ControllerBase
    {
        private readonly IWidgetTemplateService _widgetTemplateService;

        public WidgetTemplatesController(IWidgetTemplateService widgetTemplateService)
        {
            _widgetTemplateService = widgetTemplateService;
        }

        [HttpGet("widget-templates")]
        public async Task<ActionResult<IEnumerable<WidgetTemplateInfosFoundationModel>>> GetMany([FromQuery] WidgetTemplatesFilterFoundationModel filter, [FromQuery] string languageCode)
        {
            var result = await _widgetTemplateService.GetMany(languageCode, filter);
            return Ok(result);
        }
    }
}
