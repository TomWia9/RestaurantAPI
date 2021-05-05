using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Queries.File;

namespace RestaurantAPI.Handlers.File
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
