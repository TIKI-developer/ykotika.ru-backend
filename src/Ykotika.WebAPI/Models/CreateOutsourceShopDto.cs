using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateOutsourceShopDto : IMapWith<CreateOutsourceShopCommand>
    {
        public required string Name { get; set; }
        public required string Link { get; set; }
        public required string ImagePath { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOutsourceShopDto, CreateOutsourceShopCommand>();
        }
    }
}
