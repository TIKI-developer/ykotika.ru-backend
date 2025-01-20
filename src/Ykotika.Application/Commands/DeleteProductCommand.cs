using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
