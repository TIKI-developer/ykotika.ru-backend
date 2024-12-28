using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public required Guid ProductTypeId { get; set; }
        public required Guid FormRecordId { get; set; }
    }
}
