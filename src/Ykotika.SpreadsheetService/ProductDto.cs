using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.SpreadsheetService
{
    public class ProductDto : IMapWith<Product>
    {
        [CellProperty(isHyperLink: true)]
        public required string Id { get; set; }

        [CellProperty]
        public required string Article { get; set; }

        [CellProperty]
        public required string Name { get; set; }

        [CellProperty]
        public required string Description { get; set; }

        [CellProperty]
        public required List<string> Tags { get; set; }

        [CellProperty(isHyperLink: true)]
        public string? Source { get; set; }

        [CellProperty]
        public required List<string> Images { get; set; }

        [CellProperty(isHyperLink: true)]
        public required string AuthorId { get; set; }
        public required ProductType ProductType { get; set; }
        public required FormRecord FormRecord { get; set; }
        public required User User { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(to => to.AuthorId,
                opt => opt.MapFrom(from => from.User.Id))
                .ForMember(to => to.ProductType,
                opt => opt.MapFrom(from => from.ProductType))
                .ForMember(to => to.Tags,
                opt => opt.MapFrom(from => from.Tags.Select(e => e.Value)))
                .ForMember(to => to.Images,
                opt => opt.MapFrom(from => from.Images.Select(e => e.Image.Path)))
                .ForMember(to => to.Source,
                opt => opt.MapFrom(from => from.Source.Path));
        }
    }
}
