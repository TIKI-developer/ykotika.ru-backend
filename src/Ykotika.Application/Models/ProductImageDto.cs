using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Models
{
    public class ProductImageDto : IMapWith<ImageListItem>
    {
        public required int OrderIndex { get; set; }
        public required Guid FileId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImageListItem, ProductImageDto>()
                .ForMember(to => to.FileId,
                opt => opt.MapFrom(from => from.File.Id));
        }
    }
}
