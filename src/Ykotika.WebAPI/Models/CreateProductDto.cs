using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public required Guid ProductTypeId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsAdult { get; set; }
        public required List<Tag> Tags { get; set; }
        public required List<ImageListItemDto> Images { get; set; }
        public List<Guid>? CategoryIds { get; set; }
        public required CreateFormRecordDto FormRecord { get; set; }
        public required string SourcePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>();
        }
    }
}
