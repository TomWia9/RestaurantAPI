using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;

namespace Application.Files.Queries.GetFile
{
    public class GetFileHandler : IRequestHandler<GetFileQuery, byte[]>
    {
        public async Task<byte[]> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/Files/{request.FileName}";
            var fileExists = File.Exists(filePath);

            if (!fileExists) throw new NotFoundException();

            return await File.ReadAllBytesAsync(filePath, cancellationToken);
            ;
        }
    }
}