using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.File.Commands.Upload
{
    public class FileViewModel : IMapWith<FileModel>
    {
        public required Guid Id { get; set; }
        public required string RelativePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FileModel, FileViewModel>()

                .ForMember(to => to.Id,
                opt => opt.MapFrom(from => from.Id))

                .ForMember(to => to.RelativePath,
                opt => opt.MapFrom(from => Path.Combine(from.RelativePath, from.Name).Replace("\\", "/")));
        }
    }
}
