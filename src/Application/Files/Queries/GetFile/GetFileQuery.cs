using MediatR;

namespace Application.Files.Queries.GetFile
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
