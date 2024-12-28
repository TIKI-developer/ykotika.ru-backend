using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.WebAPI.Models
{
    public class SendRequestToBeAuthorDto : IMapWith<SendRequestToBeCommand>
    {
        public string? Name { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required List<Social> Socials { get; set; }
        public required string TellAboutYourself { get; set; }
        public required string WhichSocial { get; set; }
        public required bool ConfirmedOffer { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SendRequestToBeAuthorDto, SendRequestToBeCommand>()

                .ForMember(to => to.WhichSocial,
                opt => opt.MapFrom(from => (AuthorRequest.ContactSocial)Enum.Parse(typeof(AuthorRequest.ContactSocial), from.WhichSocial)));
        }
    }
}
