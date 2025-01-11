using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class CategoryDetails : IMapWith<Category>, IHasAuthor, IPublishable
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublished { get; set; }
        public required string ImagePath { get; set; }
        public required Guid AuthorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDetails>()
                .ForMember(to => to.AuthorId,
                opt => opt.MapFrom(from => from.Author.Id))
                .ForMember(to => to.ImagePath,
                opt => opt.MapFrom(from => from.Image.Path));
        }
    }
}
