using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class UserDetails : IMapWith<User>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public Domain.Entities.File? Picture { get; set; }
        public List<UserPermission>? Permissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetails>();
        }
    }
}
