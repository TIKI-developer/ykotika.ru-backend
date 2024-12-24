using AutoMapper;
using Ykotika.Application.Commands.Input;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

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
