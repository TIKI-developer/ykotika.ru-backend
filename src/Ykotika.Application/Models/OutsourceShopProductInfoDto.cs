using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Models
{
    public class OutsourceShopProductInfoDto : IMapWith<OutsourceShopProductInfo>
    {
        public required OutsourceShopDetails OutsourceShop { get; set; }
        public required string Link { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OutsourceShopProductInfo, OutsourceShopProductInfoDto>();
        }
    }
}
