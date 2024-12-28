using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Models
{
    public class ProductSpreadsheetDto : IMapWith<Product>
    {
        public required string Article {  get; set; }
        public required string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductSpreadsheetDto>()
                .ForMember(to => to.Article,
                opt => opt.MapFrom(from => from.Name));
        }
    }
}
