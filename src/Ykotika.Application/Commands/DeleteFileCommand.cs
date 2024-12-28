using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteFileCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
