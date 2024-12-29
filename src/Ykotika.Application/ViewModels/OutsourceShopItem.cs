using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class OutsourceShopItem : IMapWith<OutsourceShop>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OutsourceShop, OutsourceShopItem>();
        }
    }
}
