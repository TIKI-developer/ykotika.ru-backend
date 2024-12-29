using MediatR;
using Ykotika.Application.Models;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public required Guid Id { get; set; }
        public Guid? SourceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public required List<Tag> Tags { get; set; }
        public List<ImageListItemDto>? Images { get; set; }
    }
}
