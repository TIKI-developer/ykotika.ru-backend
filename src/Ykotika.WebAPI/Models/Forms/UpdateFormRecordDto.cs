using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.FormRecord.Commands.Update;

namespace Ykotika.WebAPI.Models.Forms
{
    public class UpdateFormRecordDto : IMapWith<UpdateFormRecordCommand>
    {
        public List<UpdateFormInputRecordDto>? InputRecords { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFormRecordDto, UpdateFormRecordCommand>();
        }
    }
}
