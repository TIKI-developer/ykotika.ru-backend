using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateUserPermissionsDto : IMapWith<UpdateUserPermissionsCommand>
    {
        public List<string>? Permissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserPermissionsDto, UpdateUserPermissionsCommand>()
                    .ForMember(to => to.Permissions,
                    opt => opt.MapFrom(from => from.Permissions.Select(e => Enum.Parse(typeof(UserPermission), e))));
        }
    }
}
