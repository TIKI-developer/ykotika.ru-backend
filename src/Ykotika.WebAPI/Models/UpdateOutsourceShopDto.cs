using AutoMapper;
using Ykotika.Application.Commands;

namespace Ykotika.WebAPI.Models
{
    public class UpdateOutsourceShopDto
    {
        public string? Name { get; set; }
        public string? Link { get; set; }
        public Guid? LogoFileId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOutsourceShopDto, UpdateOutsourceShopCommand>();
        }
    }
}
