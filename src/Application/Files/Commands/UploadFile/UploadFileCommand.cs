using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands.UploadFile
{
    public class UploadFileCommand : IRequest
    {
        public UploadFileCommand(IFormFile file)
        {
            File = file;
        }

        public IFormFile File { get; set; }
    }
}