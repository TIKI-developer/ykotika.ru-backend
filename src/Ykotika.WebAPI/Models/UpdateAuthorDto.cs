using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateAuthorDto : IMapWith<UpdateAuthorCommand>
    {
        public string? About { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAuthorCommand, UpdateAuthorDto>();
        }
    }
}
