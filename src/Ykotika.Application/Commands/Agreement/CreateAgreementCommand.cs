using MediatR;

namespace Ykotika.Application.Commands.Agreement
{
    public class CreateAgreementCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid OfferId { get; set; }
    }
}
