using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteProductTypeCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
