using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace RestaurantAPI.Commands.File
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
