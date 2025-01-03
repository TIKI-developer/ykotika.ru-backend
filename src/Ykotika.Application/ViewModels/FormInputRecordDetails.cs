using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormInputRecordDetails : IMapWith<FormRecord.InputRecord>
    {
        public required string Id { get; set; }
        public required string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormRecord.InputRecord, FormInputRecordDetails>();
        }
    }
}
