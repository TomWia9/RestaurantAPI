using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using RestaurantAPI.Commands.File;
using RestaurantAPI.Queries.File;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    [Route("file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"fileName"})]
        public async Task<IActionResult> GetFile([FromQuery] string fileName)
        {
            var result = await _mediator.Send(new GetFileQuery(fileName));

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out var contentType);

            return File(result, contentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            await _mediator.Send(new UploadFileCommand(file));
            return Ok();
        }
    }
}
