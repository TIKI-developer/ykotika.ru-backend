using MediatR;

namespace Ykotika.Application.Commands.Category
{
    public class DeleteCategoryCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
