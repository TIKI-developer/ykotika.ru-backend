using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateOfferCommand : IRequest<Guid>
    {
        public required string Content { get; set; }
    }
}
