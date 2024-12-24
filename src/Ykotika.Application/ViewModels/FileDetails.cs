using AutoMapper;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.Application.ViewModels
{
    public class FileDetails : IMapWith<Domain.Entities.File>
    {
        public required Guid Id { get; set; }
        public required string RelativePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.File, FileDetails>()

                .ForMember(to => to.Id,
                opt => opt.MapFrom(from => from.Id))

                .ForMember(to => to.RelativePath,
                opt => opt.MapFrom(from => Path.Combine(from.RelativePath, from.Name).Replace("\\", "/")));
        }
    }
}
