using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required List<Tag> Tags { get; set; }
        public List<ImageListItemDto>? Images { get; set; }
        public List<Guid>? CategoryIds { get; set; }
        public required string SourcePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>();
        }
    }
}
