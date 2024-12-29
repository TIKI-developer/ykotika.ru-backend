using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public required Guid ProductTypeId { get; set; }
        public required Guid FormRecordId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required List<string> Tags { get; set; }
        public required List<ImageListItemDto> Images { get; set; }
        public required Guid SourceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>();
        }
    }
}
