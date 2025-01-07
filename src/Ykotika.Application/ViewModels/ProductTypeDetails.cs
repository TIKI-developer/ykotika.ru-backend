using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class ProductTypeDetails : IMapWith<ProductType>
    {
        public required string Name { get; set; }
        public required string ArticlePattern { get; set; }
        public required FormDetails Form { get; set; }
        public required bool IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductType, ProductTypeDetails>();
        }
    }
}
