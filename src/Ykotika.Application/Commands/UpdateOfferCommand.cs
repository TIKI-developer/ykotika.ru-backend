using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateOfferCommand : IRequest<Guid>
    {
        public Guid AuthorId { get; set; }
        public required Guid Id { get; set; }
        public string? Content { get; set; }
        public bool? IsPublished { get; set; }
    }
}
