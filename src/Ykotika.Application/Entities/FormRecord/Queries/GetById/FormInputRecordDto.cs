using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Form.Commands.Create;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.FormRecord.Queries.GetById
{
    public class FormInputRecordDto : IMapWith<FormInputRecordModel>
    {
        public required Guid Id { get; set; }
        public required FormInputDto FormInput { get; set; }
        public required string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormInputRecordModel, FormInputRecordDto>();
        }
    }
}
