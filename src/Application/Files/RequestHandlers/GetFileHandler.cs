using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Files.Queries.GetFile;
using MediatR;

namespace Application.Files.RequestHandlers
{
    public class GetFileHandler : IRequestHandler<GetFileQuery, byte[]>
    {
        public async Task<byte[]> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/Files/{request.FileName}";
            var fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
            {
                throw new NotFoundException();
            }

            return await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);;
        }
    }
}
