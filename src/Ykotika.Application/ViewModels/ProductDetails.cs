using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class ProductDetails : IMapWith<Product>
    {
        public required Guid Id { get; set; }
        public required string Article { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required List<OutsourceShopDetails> OutsourceShops { get; set; }
        public required List<Tag> Tags { get; set; }
        public List<string>? Comments { get; set; }
        public required Domain.Entities.File Source { get; set; }
        public required ProductStatus Status { get; set; }
        public required List<ImageListItem> Images { get; set; }
        public required bool IsPublished { get; set; }
        public required ProductType ProductType { get; init; }
        public required FormRecordDetails FormRecord { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetails>();
        }
    }
}
