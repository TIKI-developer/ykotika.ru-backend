using MediatR;

namespace Ykotika.Application.Commands.Product
{
    public class DeleteProductCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
