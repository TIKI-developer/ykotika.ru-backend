using MediatR;

namespace Ykotika.Application.Entities.File.Commands.Download
{
    public class DownloadCommand : IRequest<FileData>
    {
        public required Guid Id { get; set; }
    }
}
