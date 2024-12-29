using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class ChangePasswordDto : IMapWith<ChangePasswordCommand>
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDto, ChangePasswordCommand>();
        }
    }
}
