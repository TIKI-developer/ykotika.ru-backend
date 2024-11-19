using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.User.Commands.Login;

namespace ykotika.WebAPI.Models
{
    public class LoginDto : IMapWith<LoginCommand>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, LoginCommand>()

                .ForMember(to => to.Email,
                opt => opt.MapFrom(from => from.Email))

                .ForMember(to => to.Password,
                opt => opt.MapFrom(from => from.Password));
        }
    }
}
