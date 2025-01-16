using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateOfferCommand : IRequest<Guid>
    {
        public required Guid AuthorId { get; set; }
        public required string Content { get; set; }
        public required bool IsPublished { get; set; }
    }
}
