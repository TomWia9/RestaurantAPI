using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantAPI.Queries.File
{
    public class GetFileQuery : IRequest<byte[]>
    {
        public GetFileQuery(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }
    }
}
