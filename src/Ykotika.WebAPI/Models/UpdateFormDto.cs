using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateFormDto : IMapWith<UpdateFormCommand>
    {
        public string? Name { get; set; }
        public bool? IsPublished { get; set; }
        public List<UpdateFormInputDto>? Inputs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFormDto, UpdateFormCommand>();
        }
        public class UpdateFormInputDto : IMapWith<UpdateFormCommand.InputDto>
        {
            public string? Id { get; set; }
            public string? Type { get; set; }
            public Form.InputExtraAttributes? ExtraAttributes { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<UpdateFormInputDto, UpdateFormCommand.InputDto>()
                    .ForMember(to => to.Type,
                    opt => opt.MapFrom(from => Enum.Parse(typeof(Form.InputType), from.Type)));
            }
        }
    }
}
