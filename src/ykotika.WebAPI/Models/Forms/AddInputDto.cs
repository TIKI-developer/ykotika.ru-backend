using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Entities.Form.Commands.AddInput;
using Ykotika.Domain;

namespace Ykotika.WebAPI.Models.Forms
{
    public class AddInputDto : IMapWith<AddInputCommand>
    {
        public required InputType Type { get; set; }
        public required string Label { get; set; }
        public required bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddInputDto, AddInputCommand>();
        }
    }
}
