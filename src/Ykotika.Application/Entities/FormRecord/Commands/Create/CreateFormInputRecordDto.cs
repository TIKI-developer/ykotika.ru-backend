using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.FormRecord.Commands.Create
{
    public class CreateFormInputRecordDto : IMapWith<FormInputRecordModel>
    {
        public required Guid FormInputId { get; set; }
        public required string Value { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormInputRecordDto, FormInputRecordModel>();
        }
    }
}
