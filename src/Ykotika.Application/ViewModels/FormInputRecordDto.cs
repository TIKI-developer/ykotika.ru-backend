using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class FormInputRecordDto : IMapWith<InputRecord>
    {
        public required Guid Id { get; set; }
        public required FormInputDto FormInput { get; set; }
        public required string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InputRecord, FormInputRecordDto>();
        }
    }
}
