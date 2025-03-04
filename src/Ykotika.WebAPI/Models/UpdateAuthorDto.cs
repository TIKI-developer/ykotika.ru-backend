using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.WebAPI.Models
{
    public class UpdateAuthorDto : IMapWith<UpdateAuthorCommand>
    {
        public List<Social>? Socials { get; set; }
        public string? About { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAuthorCommand, UpdateAuthorDto>();
        }
    }
}
