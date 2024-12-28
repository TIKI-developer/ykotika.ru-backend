using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.ViewModels;

namespace Ykotika.WebAPI.Models
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
