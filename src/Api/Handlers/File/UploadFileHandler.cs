using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Commands.File;
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Handlers.File
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
