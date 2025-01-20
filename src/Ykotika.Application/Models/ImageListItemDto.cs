using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Models
{
    public class ImageListItemDto : IMapWith<ImageListItem>
    {
        public required int OrderIndex { get; set; }
        public required string ImagePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImageListItem, ImageListItemDto>()
                .ForMember(to => to.ImagePath,
                opt => opt.MapFrom(from => from.Image.Path));
        }
    }
}
