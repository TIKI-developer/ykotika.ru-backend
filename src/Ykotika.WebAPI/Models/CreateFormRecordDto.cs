using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.ViewModels;

namespace Ykotika.WebAPI.Models
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
