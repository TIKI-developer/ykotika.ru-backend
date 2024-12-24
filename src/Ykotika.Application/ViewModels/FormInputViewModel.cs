using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormInputViewModel : IMapWith<Input>
    {
        public required Guid Id { get; set; }
        public required string Label { get; set; }
        public required InputType Type { get; set; }
        public bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormInputViewModel, Input>();
            profile.CreateMap<Input, FormInputViewModel>();
        }
    }
}
