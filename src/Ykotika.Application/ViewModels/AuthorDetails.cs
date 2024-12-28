using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class AuthorDetails : IMapWith<Author>
    {
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public Domain.Entities.File? Picture { get; set; }
        public required List<Social> Socials { get; set; }
        public required AuthorStatus Status { get; set; }
        public required string TellAboutYourself { get; set; }
        public required Social WhichSocial { get; set; }
        public required Timestamps Timestamps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDetails>()
                .ForMember(to => to.Name,
                opt => opt.MapFrom(from => from.User.Name))
                .ForMember(to => to.Surname,
                opt => opt.MapFrom(from => from.User.Surname))
                .ForMember(to => to.PhoneNumber,
                opt => opt.MapFrom(from => from.User.PhoneNumber))
                .ForMember(to => to.Email,
                opt => opt.MapFrom(from => from.User.Email))
                .ForMember(to => to.Picture,
                opt => opt.MapFrom(from => from.User.Picture))
                .ForMember(to => to.TellAboutYourself,
                opt => opt.MapFrom(from => from.Request.TellAboutYourself))
                .ForMember(to => to.WhichSocial,
                opt => opt.MapFrom(from => from.Request.WhichSocial))
                .ForMember(to => to.Timestamps,
                opt => opt.MapFrom(from => from.Request.Timestamps));
        }
    }
}
