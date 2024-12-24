using AutoMapper;
using Ykotika.Application.Commands.Form;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.ViewModels;

namespace Ykotika.WebAPI.Models.Forms
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
