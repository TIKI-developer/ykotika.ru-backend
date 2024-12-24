using AutoMapper;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.Application.ViewModels
{
    public class ProfileViewModel : IMapWith<Domain.Entities.User>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.User, ProfileViewModel>();
        }
    }
}
