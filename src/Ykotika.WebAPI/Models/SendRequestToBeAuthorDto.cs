using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Author.Commands;
using Ykotika.Domain;

namespace Ykotika.WebAPI.Models
{
    public class SendRequestToBeAuthorDto : IMapWith<SendRequestToBeCommand>
    {
        public string? UserName { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Socials { get; set; }
        public required string TellAboutYourself { get; set; }
        public required Social WhichSocial { get; set; }
        public required bool ConfirmedOffer { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SendRequestToBeAuthorDto, SendRequestToBeCommand>();
        }
    }
}
