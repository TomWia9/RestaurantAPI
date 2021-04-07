using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    [Route("file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/Files/{fileName}";
            var fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
            {
                return NotFound();
            }

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out var contentType);

            var fileContents = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileContents, contentType, fileName);
        }
    }
}
