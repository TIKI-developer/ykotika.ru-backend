using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormInputeDetails : IMapWith<Input>
    {
        public required Guid Id { get; set; }
        public required string Label { get; set; }
        public required InputType Type { get; set; }
        public bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormInputeDetails, Input>();
            profile.CreateMap<Input, FormInputeDetails>();
        }
    }
}
