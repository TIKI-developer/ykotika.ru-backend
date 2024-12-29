using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Models
{
    public class ImageListItemDto : IMapWith<ImageListItem>
    {
        public required int OrderIndex { get; set; }
        public required Guid FileId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImageListItem, ImageListItemDto>()
                .ForMember(to => to.FileId,
                opt => opt.MapFrom(from => from.File.Id));
        }
    }
}
