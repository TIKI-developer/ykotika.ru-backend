using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductOutsourceShopDto : IMapWith<UpdateProductOutsourceShopCommand>
    {
        public required Guid Id { get; set; }
        public required List<OutsourceShopLinkDto> OutsourceShopInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductOutsourceShopDto, UpdateProductOutsourceShopCommand>();
        }
        public class OutsourceShopLinkDto : IMapWith<UpdateProductOutsourceShopCommand.OutsourceShopLinkDto>
        {
            public required Guid OutsourceShopId { get; set; }
            public required string Link { get; set; }
            public void Mapping(Profile profile)
            {
                profile.CreateMap<OutsourceShopLinkDto, UpdateProductOutsourceShopCommand.OutsourceShopLinkDto>();
            }
        }
    }
}
