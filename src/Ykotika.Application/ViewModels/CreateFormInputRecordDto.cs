using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class CreateFormInputRecordDto : IMapWith<InputRecord>
    {
        public required Guid FormInputId { get; set; }
        public required string Value { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormInputRecordDto, InputRecord>();
        }
    }
}
