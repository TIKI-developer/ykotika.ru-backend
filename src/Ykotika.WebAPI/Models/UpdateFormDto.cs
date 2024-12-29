using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateFormDto : IMapWith<UpdateFormCommand>
    {
        public string? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFormDto, UpdateFormCommand>();
        }
    }
}
