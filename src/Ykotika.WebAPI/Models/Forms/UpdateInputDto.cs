using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Form.Commands.UpdateInput;

namespace Ykotika.WebAPI.Models.Forms
{
    public class UpdateInputDto : IMapWith<UpdateInputCommand>
    {
        public string? Label { get; set; }
        public bool? IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateInputDto, UpdateInputCommand>();
        }
    }
}
