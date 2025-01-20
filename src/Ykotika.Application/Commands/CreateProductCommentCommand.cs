using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommentCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Content { get; set; }
    }
}
