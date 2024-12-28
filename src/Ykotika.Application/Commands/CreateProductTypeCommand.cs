using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductTypeCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
    }
}
