using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductTypeDto : IMapWith<UpdateProductTypeCommand>
    {
        public string? Name { get; set; }
        public List<string>? ArticlePattern { get; set; }
        public bool? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductTypeDto, UpdateProductTypeCommand>();
        }
    }
}
