using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormDetails : IMapWith<Form>
    {
        public required string Name { get; set; }
        public required List<FormInputeDetails> Fields { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form, FormDetails>()
                .ForMember(to => to.Fields,
                opt => opt.MapFrom(from => from.Inputs));
        }
    }
}
