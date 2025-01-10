using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class FormRecordDetails : IMapWith<FormRecord>
    {
        public required Guid Id { get; set; }
        public required string FormName { get; set; }
        public required UserDetails Author { get; set; }
        public List<FormInputRecordDetails> InputRecords { get; set; } = [];
        public required Timestamps Timestamps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormRecord, FormRecordDetails>()
                .ForMember(to => to.FormName,
                opt => opt.MapFrom(from => from.Form.Name));
        }
    }
}
