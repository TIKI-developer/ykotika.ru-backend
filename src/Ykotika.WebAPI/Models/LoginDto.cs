using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.User.Commands.Login;

namespace Ykotika.WebAPI.Models
{
    public class LoginDto : IMapWith<LoginCommand>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, LoginCommand>();
        }
    }
}
