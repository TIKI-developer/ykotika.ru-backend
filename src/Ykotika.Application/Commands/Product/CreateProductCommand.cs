using MediatR;

namespace Ykotika.Application.Commands.Product
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public required Guid CategoryId { get; set; }
        public required Guid FormRecordId { get; set; }
    }
}
