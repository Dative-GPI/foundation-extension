using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Controllers
{
    [Route("api/core/v1/organisations/{organisationId:Guid}/widget-templates")]
    public class WidgetTemplatesController : ControllerBase
    {
        private readonly IWidgetTemplateService _widgetTemplateService;

        public WidgetTemplatesController(IWidgetTemplateService widgetTemplateService)
        {
            _widgetTemplateService = widgetTemplateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WidgetTemplateInfosFoundationModel>>> GetMany([FromQuery] WidgetTemplatesFilterFoundationModel filter)
        {
            var result = await _widgetTemplateService.GetMany(filter);
            return Ok(result);
        }
    }
}
