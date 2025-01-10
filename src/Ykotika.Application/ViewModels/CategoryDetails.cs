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
        public required bool IsPublished { get; set; }
        public required string ImagePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDetails>()
                .ForMember(to => to.ImagePath,
                opt => opt.MapFrom(from => from.Image.Path));
        }
    }
}
