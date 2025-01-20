using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateFormRecordDto : IMapWith<UpdateFormRecordCommand>
    {
        public List<UpdateFormRecordInputDto>? InputRecords { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFormRecordDto, UpdateFormRecordCommand>();
        }
        public class UpdateFormRecordInputDto : IMapWith<UpdateFormRecordCommand.InputRecordDto>
        {
            public required string Id { get; set; }
            public required string Value { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<UpdateFormRecordInputDto, UpdateFormRecordCommand.InputRecordDto>();
            }
        }
    }
}
