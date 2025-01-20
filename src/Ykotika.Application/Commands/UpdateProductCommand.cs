using MediatR;
using Ykotika.Application.Models;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? SourcePath { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public UpdateFormRecordCommand? FormRecord { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Guid>? CategoryIds { get; set; }
        public List<ImageListItemDto>? Images { get; set; }
    }
}
