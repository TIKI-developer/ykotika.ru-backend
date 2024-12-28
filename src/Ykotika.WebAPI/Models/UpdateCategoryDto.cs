using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? ImageFileId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
        }
    }
}
