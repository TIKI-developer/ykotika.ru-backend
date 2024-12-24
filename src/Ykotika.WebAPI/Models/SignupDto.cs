using AutoMapper;
using Ykotika.Application.Commands.User;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class SignupDto : IMapWith<SignupCommand>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignupDto, SignupCommand>();
        }
    }
}
