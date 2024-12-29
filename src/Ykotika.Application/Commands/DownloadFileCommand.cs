using MediatR;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class DownloadFileCommand : IRequest<FileData>
    {
        public required Guid Id { get; set; }
    }
}
