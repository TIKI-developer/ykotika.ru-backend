using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormRecordDetails : IMapWith<FormRecord>
    {
        public required Guid Id { get; set; }
        public required string FormName { get; set; }
        public List<FormInputRecordDto> InputRecords { get; set; } = [];
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormRecord, FormRecordDetails>()
                .ForMember(to => to.FormName,
                opt => opt.MapFrom(from => from.Form.Name));
        }
    }
}
