using MediatR;

namespace Ykotika.Application.Entities.File.Commands.Delete
{
    public class DeleteCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
