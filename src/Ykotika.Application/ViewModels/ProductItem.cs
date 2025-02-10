using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class ProductItem : IMapWith<Product>, IPublishable
    {
        public required Guid Id { get; set; }
        public required string Article { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<OutsourceShopProductInfoDto>? OutsourceShops { get; set; }
        public required bool IsAdult { get; set; }
        public List<ImageListItem>? Images { get; set; }
        public required bool IsPublished { get; set; }
        public required ProductType ProductType { get; init; }
        public required UserDetails User { get; set; }
        public required Timestamps Timestamps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductItem>();
        }
    }
}
