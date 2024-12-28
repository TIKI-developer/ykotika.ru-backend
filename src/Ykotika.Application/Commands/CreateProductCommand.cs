using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public required Guid ProductTypeId { get; set; }
        public required Guid FormRecordId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
