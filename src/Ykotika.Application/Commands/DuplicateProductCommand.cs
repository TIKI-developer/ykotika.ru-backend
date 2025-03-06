using MediatR;

namespace Ykotika.Application.Commands
{
    public class DuplicateProductCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
    }
}
