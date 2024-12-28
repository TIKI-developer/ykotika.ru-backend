using MediatR;

namespace Ykotika.Application.Commands.Offer
{
    public class CreateOfferCommand : IRequest<Guid>
    {
        public required string Content { get; set; }
    }
}
