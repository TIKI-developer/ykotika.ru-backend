using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublished { get; set; }
        public required string ImagePath { get; set; }
    }
}
