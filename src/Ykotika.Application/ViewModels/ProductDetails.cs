using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class ProductDetails : IMapWith<Product>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<OutsourceShopDetails>? OutsourceShops { get; set; }
        public List<Domain.Entities.File>? Images { get; set; }
        public required bool IsPublished { get; set; }
        public required FormRecordDetails FormRecord { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetails>();
        }
    }
}
