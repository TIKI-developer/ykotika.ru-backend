using AutoMapper;
using Ykotika.Application.Commands.User;
using Ykotika.Application.Common.Mappings;

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
