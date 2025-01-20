using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.WebAPI.Models
{
    public class SendRequestToBeAuthorDto : IMapWith<SendRequestToBeAuthorCommand>
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required List<Social> Socials { get; set; }
        public required string TellAboutYourself { get; set; }
        public required string ContactSocial { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SendRequestToBeAuthorDto, SendRequestToBeAuthorCommand>()

                .ForMember(to => to.ContactSocial,
                opt => opt.MapFrom(from =>
                (AuthorRequest.ContactSocial)Enum.Parse(typeof(AuthorRequest.ContactSocial), from.ContactSocial)));
        }
    }
}
