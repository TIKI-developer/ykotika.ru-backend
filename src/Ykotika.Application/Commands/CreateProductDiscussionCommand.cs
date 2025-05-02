using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductDiscussionCommand : IRequest<Guid>
    {
        public required Guid ProductId { get; set; }
        public required Guid CreatorId { get; set; }
    }
}
