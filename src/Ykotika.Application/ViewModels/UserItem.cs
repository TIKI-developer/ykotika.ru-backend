using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class UserItem : IMapWith<User>
    {
        public required Guid Id { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Initials { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserItem>()
                .ForMember(to => to.Initials,
                opt => opt.MapFrom(from => $"{from.Surname} {from.Name}"));
        }
    }
}
