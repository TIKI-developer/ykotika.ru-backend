using MediatR;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Author.Commands
{
    public class SendRequestToBeCommand : IRequest<Unit>
    {
        public required Guid UserId { get; set; }
        public string? UserName { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Socials { get; set; }
        public required string TellAboutYourself { get; set; }
        public required Social WhichSocial { get; set; }
        public required bool ConfirmedOffer { get; set; }
    }
}
