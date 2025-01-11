using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateUserRolesDto : IMapWith<UpdateUserRolesCommand>
    {
        public List<string>? Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserRolesDto, UpdateUserRolesCommand>()
                    .ForMember(to => to.Roles,
                    opt => opt.MapFrom(from => from.Roles.Select(e => Enum.Parse(typeof(UserRole), e))));
        }
    }
}
