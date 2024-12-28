using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.ViewModels;

namespace Ykotika.WebAPI.Models
{
    public class CreateFormDto : IMapWith<CreateFormCommand>
    {
        public required string Name { get; set; }
        public List<FormInputDto>? Inputs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormDto, CreateFormCommand>();
        }
    }
}
