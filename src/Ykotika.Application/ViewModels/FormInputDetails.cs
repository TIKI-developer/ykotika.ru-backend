using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormInputDetails : IMapWith<Form.Input>
    {
        public required string Id { get; set; }
        public required string Type { get; set; }
        public string? DefaultValue { get; set; }
        public required Form.InputExtraAttributes ExtraAttributes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form.Input, FormInputDetails>()
                    .ForMember(to => to.Type,
                    opt => opt.MapFrom(from => from.Type.ToString()));
        }
    }
}
