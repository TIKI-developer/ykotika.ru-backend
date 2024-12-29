using MediatR;
using Ykotika.Application.Models;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public required Guid ProductTypeId { get; set; }
        public required Guid FormRecordId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required List<string> Tags { get; set; }
        public required List<ImageListItemDto> Images { get; set; }
        public required Guid SourceId { get; set; }
    }
}
