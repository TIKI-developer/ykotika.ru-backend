using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateOutsourceShopDto : IMapWith<UpdateOutsourceShopCommand>
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
