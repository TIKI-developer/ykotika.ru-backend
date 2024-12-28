using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
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
