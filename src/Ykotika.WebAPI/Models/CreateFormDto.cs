using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class CreateFormDto : IMapWith<CreateFormCommand>
    {
        public required string Name { get; set; }
        public required List<CreateFormInputDto> Inputs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormDto, CreateFormCommand>();
        }
        public class CreateFormInputDto : IMapWith<CreateFormCommand.InputDto>
        {
            public required string Label { get; set; }
            public required string Placeholder { get; set; }
            public required Form.InputType Type { get; set; }
            public required bool IsRequired { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<CreateFormInputDto, CreateFormCommand.InputDto>();
            }
        }
    }
}
