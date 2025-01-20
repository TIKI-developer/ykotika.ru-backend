using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class CreateFormDto : IMapWith<CreateFormCommand>
    {
        public required string Name { get; set; }
        public required bool IsPublished { get; set; }
        public required List<CreateFormInputDto> Inputs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormDto, CreateFormCommand>();
        }
        public class CreateFormInputDto : IMapWith<CreateFormCommand.InputDto>
        {
            public required string Type { get; set; }
            public required Form.InputExtraAttributes ExtraAttributes { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<CreateFormInputDto, CreateFormCommand.InputDto>()
                    .ForMember(to => to.Type,
                    opt => opt.MapFrom(from => Enum.Parse(typeof(Form.InputType), from.Type)));
            }
        }
    }
}
