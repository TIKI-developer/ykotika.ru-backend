using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.User.Commands.Signup;

namespace ykotika.WebAPI.Models
{
    public class SignupDto : IMapWith<SignupCommand>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignupDto, SignupCommand>()

                .ForMember(to => to.Name,
                opt => opt.MapFrom(from => from.Name))

                .ForMember(to => to.Email,
                opt => opt.MapFrom(from => from.Email))

                .ForMember(to => to.Password,
                opt => opt.MapFrom(from => from.Password));
        }
    }
}
