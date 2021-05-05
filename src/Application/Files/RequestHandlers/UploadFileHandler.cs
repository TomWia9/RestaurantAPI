using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Files.Commands.UploadFile;
using MediatR;

namespace Application.Files.RequestHandlers
{
    public class UploadFileHandler : IRequestHandler<UploadFileCommand>
    {
        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length <= 0)
                throw new BadRequestException();

            var rootPath = Directory.GetCurrentDirectory();
            var fileName = request.File.FileName;
            var fullPath = $"{rootPath}/Files/{fileName}";

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await request.File.CopyToAsync(stream, cancellationToken);

            return Unit.Value;
        }
    }
}
