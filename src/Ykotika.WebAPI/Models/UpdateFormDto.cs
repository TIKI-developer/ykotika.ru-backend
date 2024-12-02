using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Form.Commands.Update;

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
