using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class ChangeAuthorStatusDto : IMapWith<ChangeAuthorStatusCommand>
    {
        public required string NewStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeAuthorStatusDto, ChangeAuthorStatusCommand>()
                .ForMember(to => to.NewStatus,
                opt => opt.MapFrom(from => Enum.Parse(typeof(AuthorStatus), from.NewStatus)));
        }
    }
}