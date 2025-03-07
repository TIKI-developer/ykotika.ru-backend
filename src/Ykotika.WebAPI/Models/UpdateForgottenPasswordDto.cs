using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateForgottenPasswordDto : IMapWith<ResetPasswordCommand>
    {
        public required string NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateForgottenPasswordDto, ResetPasswordCommand>();
        }
    }
}
