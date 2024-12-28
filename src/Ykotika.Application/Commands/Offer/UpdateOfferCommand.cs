using MediatR;

namespace Ykotika.Application.Commands.Offer
{
    public class UpdateOfferCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Content { get; set; }
    }
}
