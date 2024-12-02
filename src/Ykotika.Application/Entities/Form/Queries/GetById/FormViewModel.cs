using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Form.Commands.Create;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Queries.GetById
{
    public class FormViewModel : IMapWith<FormModel>
    {
        public required string Name { get; set; }
        public required List<FormInputViewModel> Fields { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormModel, FormViewModel>()
                .ForMember(to => to.Fields,
                opt => opt.MapFrom(from => from.Inputs));
        }
    }
}
