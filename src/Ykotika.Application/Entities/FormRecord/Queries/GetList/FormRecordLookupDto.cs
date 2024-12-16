using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.FormRecord.Queries.GetList
{
    public class FormRecordLookupDto : IMapWith<FormRecordModel>
    {
        public required Guid Id { get; set; }
        public required string FormName { get; set; }
        public required DateTime UpdatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormRecordModel, FormRecordLookupDto>()
                .ForMember(to => to.FormName,
                opt => opt.MapFrom(from => from.Form.Name));
        }
    }
}
