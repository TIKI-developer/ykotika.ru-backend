using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateFormRecordDto : IMapWith<UpdateFormRecordCommand>
    {
        public List<CreateFormRecordInputDto>? InputRecords { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFormRecordDto, UpdateFormRecordCommand>();
        }
        public class CreateFormRecordInputDto : IMapWith<UpdateFormRecordCommand.InputRecordDto>
        {
            public required int Id { get; set; }
            public required string Value { get; set; }
        }
    }
}
