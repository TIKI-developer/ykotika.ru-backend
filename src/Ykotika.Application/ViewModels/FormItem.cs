using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormItem : IMapWith<Form>
    {
        public required Guid Id { get; set; }
        public required bool IsPublished { get; set; }
        public required string Name { get; set; }
        public required List<FormInputDetails> Inputs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form, FormItem>()
                .ForMember(to => to.Inputs, opt => opt.MapFrom(from => from.Inputs.OrderBy(i => i.OrderIndex)));
        }
    }
}
