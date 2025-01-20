using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteFileCommand : IRequest
    {
        public required string Path { get; set; }
    }
}
