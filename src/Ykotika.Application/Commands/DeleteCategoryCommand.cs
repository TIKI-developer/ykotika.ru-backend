using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
