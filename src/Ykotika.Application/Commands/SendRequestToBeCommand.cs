using MediatR;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class SendRequestToBeCommand : IRequest<Unit>
    {
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required List<Social> Socials { get; set; }
        public required string TellAboutYourself { get; set; }
        public required AuthorRequest.ContactSocial WhichSocial { get; set; }
        public required bool ConfirmedOffer { get; set; }
    }
}
