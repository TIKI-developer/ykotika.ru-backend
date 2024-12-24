using MediatR;

namespace Ykotika.Application.Commands.File
{
    public class DeleteFileCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
