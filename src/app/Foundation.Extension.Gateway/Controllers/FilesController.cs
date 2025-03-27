using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using Foundation.Extension.Gateway.Abstractions;
using Foundation.Extension.Gateway.ViewModels;

namespace Foundation.Extension.Gateway.Controllers
{
    [Route("api/shared/v1/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 60 * 60 * 24 * 30)]
        public async Task<FileContentResult> Get([FromRoute] Guid id)
        {
            var result = await _fileService.Get(id);

            var contentDisposition = new ContentDispositionHeaderValue("inline")
            {
                FileName = result.Label
            };

            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();

            return File(result.Data, result.ContentType);
        }
    }
}
