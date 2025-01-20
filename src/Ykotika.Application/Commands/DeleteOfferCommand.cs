using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteOfferCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
