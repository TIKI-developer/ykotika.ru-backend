using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateAuthorStatusDto : IMapWith<UpdateAuthorStatusCommand>
    {
        public required string NewStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAuthorStatusDto, UpdateAuthorStatusCommand>()
                .ForMember(to => to.NewStatus,
                opt => opt.MapFrom(from => Enum.Parse(typeof(AuthorStatus), from.NewStatus)));
        }
    }
}