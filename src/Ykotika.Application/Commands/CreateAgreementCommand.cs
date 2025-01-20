using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateAgreementCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid OfferId { get; set; }
    }
}
