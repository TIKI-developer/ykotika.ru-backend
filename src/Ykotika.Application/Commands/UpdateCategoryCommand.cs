using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsPublished { get; set; }
        public string? ImagePath { get; set; }
    }
}
