using MediatR;

namespace Ykotika.Application.Commands.Offer
{
    public class DeleteOfferCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
