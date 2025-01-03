using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateFormDto : IMapWith<UpdateFormCommand>
    {
        public string? Name { get; set; }
        public List<UpdateFormInputDto>? Inputs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFormDto, UpdateFormCommand>();
        }
        public class UpdateFormInputDto : IMapWith<UpdateFormCommand.InputDto>
        {
            public required string Id { get; set; }
            public required string Label { get; set; }
            public required string Placeholder { get; set; }
            public required Form.InputType Type { get; set; }
            public bool IsRequired { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<UpdateFormInputDto, UpdateFormCommand.InputDto>();
            }
        }
    }
}
