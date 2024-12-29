using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid ImageFileId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        }
    }
}
