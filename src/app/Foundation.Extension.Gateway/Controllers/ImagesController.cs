using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Controllers
{
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageFileService)
        {
            _imageService = imageFileService;
        }

        [HttpGet("api/shared/v1/images/{id:guid}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 60 * 60 * 24 * 30)]
        public async Task<ActionResult<ImageDetailsViewModel>> Get([FromRoute] Guid id)
        {
            var result = await _imageService.Get(id);
            return Ok(result);
        }


        [ResponseCache(Duration = 3600)]
        [HttpGet("api/v1/images/raw/{id:Guid}")] // keep for backward compatibility
        [HttpGet("api/shared/v1/images/{id:Guid}/raw")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRaw([FromRoute] Guid id)
        {
            var result = await _imageService.GetRaw(id);
            return File(result, "image/png");
        }

        [ResponseCache(Duration = 3600)]
        [HttpGet("api/v1/images/thumbnail/{id:Guid}")] // keep for backward compatibility
        [HttpGet("api/shared/v1/images/{id:Guid}/thumbnail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetThumbnail([FromRoute] Guid id)
        {
            var result = await _imageService.GetThumbnail(id);
            return File(result, "image/png");
        }
    }
}
