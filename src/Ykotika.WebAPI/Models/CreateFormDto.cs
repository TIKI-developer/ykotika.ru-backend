using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Form.Commands.Create;

namespace Ykotika.WebAPI.Models
{
    public class CreateFormDto : IMapWith<CreateFormCommand>
    {
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormDto, CreateFormCommand>();
        }
    }
}
