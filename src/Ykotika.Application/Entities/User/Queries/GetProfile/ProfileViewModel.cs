using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Queries.GetProfile
{
    public class ProfileViewModel : IMapWith<UserModel>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserModel, ProfileViewModel>();
        }
    }
}
