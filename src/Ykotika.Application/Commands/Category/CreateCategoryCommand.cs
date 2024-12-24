using MediatR;

namespace Ykotika.Application.Commands.Category
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
    }
}
