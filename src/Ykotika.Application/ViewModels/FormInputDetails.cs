using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormInputDetails : IMapWith<Form.Input>
    {
        public required string Id { get; set; }
        public required string Label { get; set; }
        public required string Placeholder { get; set; }
        public required Form.InputType Type { get; set; }
        public bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form.Input, FormInputDetails>();
        }
    }
}
