using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormRecordItem : IMapWith<FormRecord>
    {
        public required Guid Id { get; set; }
        public required string FormName { get; set; }
        public required DateTime UpdatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormRecord, FormRecordItem>()
                .ForMember(to => to.FormName,
                opt => opt.MapFrom(from => from.Form.Name));
        }
    }
}
