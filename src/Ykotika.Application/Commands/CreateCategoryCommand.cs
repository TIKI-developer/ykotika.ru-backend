using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
    }
}
