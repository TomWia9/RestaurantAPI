using System.Threading.Tasks;
using Application.Files.Commands.UploadFile;
using Application.Files.Queries.GetFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Api.Controllers
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

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="fileName">Name of file you want to download</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"fileName"})]
        public async Task<IActionResult> GetFile([FromQuery] string fileName)
        {
            var result = await _mediator.Send(new GetFileQuery(fileName));

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out var contentType);

            return File(result, contentType, fileName);
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file">File to upload</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            await _mediator.Send(new UploadFileCommand(file));
            return Ok();
        }
    }
}
