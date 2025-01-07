using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class CategoryDetails : IMapWith<Category>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Domain.Entities.File? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDetails>();
        }
    }
}
