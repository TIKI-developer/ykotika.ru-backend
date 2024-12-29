using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class CategoryItem : IMapWith<Category>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required Domain.Entities.File? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryItem>();
        }
    }
}
