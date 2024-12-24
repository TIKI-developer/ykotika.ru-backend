using AutoMapper;
using Ykotika.Application.Commands.Input;
using Ykotika.Application.Common.Mappings;

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
