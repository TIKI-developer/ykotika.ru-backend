using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class ProductDetails : IMapWith<Product>, IPublishable, IHasAuthor
    {
        public required Guid Id { get; set; }
        public required string Article { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsAdult { get; set; }
        public required List<OutsourceShopProductInfoDto> OutsourceShops { get; set; }
        public required List<Tag> Tags { get; set; }
        public List<CommentDetails>? Comments { get; set; }
        public required string SourcePath { get; set; }
        public required string Status { get; set; }
        public required List<ImageListItemDto> Images { get; set; }
        public required bool IsPublished { get; set; }
        public List<CategoryItem>? Categories { get; set; }
        public required ProductTypeDetails ProductType { get; init; }
        public required FormRecordDetails FormRecord { get; set; }
        public required Guid UserId { get; set; }
        public required UserDetails User { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetails>()
                .ForMember(to => to.Status,
                opt => opt.MapFrom(from => from.Status.ToString()))
                .ForMember(to => to.UserId,
                opt => opt.MapFrom(from => from.User.Id))
                .ForMember(to => to.SourcePath,
                opt => opt.MapFrom(from => from.Source.Path));
        }
    }
}
