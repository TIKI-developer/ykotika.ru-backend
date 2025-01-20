using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Tag>? Tags { get; set; }
        public UpdateFormRecordDto? FormRecord { get; set; }
        public List<ImageListItemDto>? Images { get; set; }
        public List<Guid>? CategoryIds { get; set; }
        public string? SourcePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>();
        }
    }
}
