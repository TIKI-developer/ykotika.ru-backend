using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductTypeDto : IMapWith<CreateProductTypeCommand>
    {
        public required string Name { get; set; }
        public required Guid FormId { get; set; }
        public string? ManualLink { get; set; }
        public required List<string> ArticlePattern { get; set; }
        public required bool IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductTypeDto, CreateProductTypeCommand>();
        }
    }
}
