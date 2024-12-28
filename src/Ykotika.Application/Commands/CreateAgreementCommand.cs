using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateAgreementCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid OfferId { get; set; }
    }
}
