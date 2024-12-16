using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Queries.GetById
{
    public class FormInputViewModel : IMapWith<FormInputModel>
    {
        public required Guid Id { get; set; }
        public required string Label { get; set; }
        public required InputType Type { get; set; }
        public bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormInputViewModel, FormInputModel>();
            profile.CreateMap<FormInputModel, FormInputViewModel>();
        }
    }
}
