using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class OutsourceShopDetails : IMapWith<OutsourceShop>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Link { get; set; }
        public required Domain.Entities.File Logo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OutsourceShop, OutsourceShopDetails>();
        }
    }
}
