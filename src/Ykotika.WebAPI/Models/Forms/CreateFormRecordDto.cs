using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.FormRecord.Commands.Create;

namespace Ykotika.WebAPI.Models.Forms
{
    public class CreateFormRecordDto : IMapWith<CreateFormRecordCommand>
    {
        public required Guid FormId { get; set; }
        public required List<CreateFormInputRecordDto> InputRecords { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFormRecordDto, CreateFormRecordCommand>();
        }
    }
}
