using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.Create
{
    public class FormInputDto : IMapWith<FormInputModel>
    {
        public required string Label { get; set; }
        public required InputType Type { get; set; }
        public bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormInputDto, FormInputModel>();
            profile.CreateMap<FormInputModel, FormInputDto>();
        }
    }
}
