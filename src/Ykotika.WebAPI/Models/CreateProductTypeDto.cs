using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductTypeDto : IMapWith<CreateProductTypeCommand>
    {
        public required Guid FormId { get; set; }
        public required string ArticlePattern { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductTypeDto, CreateProductTypeCommand>();
        }
    }
}
