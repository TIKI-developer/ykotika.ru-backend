using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProfileDto : IMapWith<UpdateProfileCommand>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? UserPictureFileId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProfileDto, UpdateProfileCommand>();
        }
    }
}
