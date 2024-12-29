using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class ChangeUserPermissionsDto : IMapWith<ChangeUserPermissionsCommand>
    {
        public List<UserPermission>? Permissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeUserPermissionsDto, ChangeUserPermissionsCommand>();
        }
    }
}
